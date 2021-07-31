using Nuke.Common;
using Nuke.Common.Execution;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.Coverlet;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Tools.GitVersion;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.IO.PathConstruction;
using static Nuke.Common.Tools.DotNet.DotNetTasks;


[CheckBuildProjectConfigurations]
[UnsetVisualStudioEnvironmentVariables]
class Build : NukeBuild
{
    public static int Main() => Execute<Build>(x => x.PushNugetPackages);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    [Solution] readonly Solution Solution;
    [GitVersion(Framework = "netcoreapp3.1")] GitVersion GitVersion;
    [Parameter] string TestResultDirectory = RootDirectory + "/Artifacts/Test-Results/";
    [Parameter] string NugetOutputDirectory = RootDirectory + "/Artifacts/NugetPackages/";
    [Parameter] string NugetServerUrl = "https://api.nuget.org/v3/index.json";
    [Parameter] string NugetApiKey;

    Target Information => _ => _
        .Before(Preparation)
        .Executes(() =>
        {
            Logger.Info($"Configuration : {Configuration}");
            Logger.Info($"TestResultDirectory : {TestResultDirectory}");
            Logger.Info($"NugetOutputDirectory : {NugetOutputDirectory}");
            Logger.Info($"NugetOutputDirectory : {NugetServerUrl}");
            Logger.Info($"GitVersion.NuGetVersionV2 : {GitVersion.NuGetVersionV2}");
        });

    Target Preparation => _ => _
        .DependsOn(Information)
        .Executes(() =>
        {
            EnsureCleanDirectory(TestResultDirectory);
            EnsureCleanDirectory(NugetOutputDirectory);
        });

    Target Clean => _ => _
        .DependsOn(Preparation)
        .Executes(() =>
        {
            DotNetClean(a =>
                a.SetProject(Solution)
                    .SetConfiguration(Configuration));
            EnsureCleanDirectory(NugetOutputDirectory);
        });

    Target Restore => _ => _
        .DependsOn(Clean)
        .Executes(() =>
        {
            DotNetRestore(a => a.SetProjectFile(Solution));
        });

    Target UpdateVersion => _ => _
        .DependsOn(Restore)
        .Executes(() =>
        {
            const string pattern = @"<Version>(.*)<\/Version>";
            const string fileName = "Directory.Build.Props";
            var filePath = Path.Combine(Solution.Directory, fileName);
            var content = GetFileContent(filePath);
            var version = "<Version>" + GitVersion.AssemblySemVer + "</Version>";
            content = Regex.Replace(content, pattern, version);
            File.WriteAllText(filePath, content, Encoding.UTF8);
        });


    Target Compile => _ => _
        .DependsOn(UpdateVersion)
        .Executes(() =>
        {
            DotNetBuild(a =>
                a.SetProjectFile(Solution)
                    .SetNoRestore(true)
                    .SetConfiguration(Configuration));
        });

    Target RunUnitTests => _ => _
        .DependsOn(Compile)
        .Executes(() =>
        {
            var testProjects = Solution.AllProjects.Where(s => s.Name.Contains("Tests.Unit"));

            DotNetTest(a => a
                .SetConfiguration(Configuration)
                .SetNoBuild(true)
                .SetNoRestore(true)
                .ResetVerbosity()
                .SetResultsDirectory(TestResultDirectory)
                    .EnableCollectCoverage()
                    .SetCoverletOutputFormat(CoverletOutputFormat.opencover)
                    .SetExcludeByFile("*.Generated.cs")
                    .EnableUseSourceLink()
                .CombineWith(testProjects, (b, z) => b
                    .SetProjectFile(z)
                    .SetLogger($"trx;LogFileName={z.Name}.trx")
                    .SetCoverletOutput(TestResultDirectory + $"{z.Name}.xml")));
        });

    Target PackNugetPackages => _ => _
        .DependsOn(RunUnitTests)
        .Executes(() =>
        {
            var projectsToPack = Solution.AllProjects
                .Where(s => !s.Name.Contains("Tests", StringComparison.OrdinalIgnoreCase)
                            && !s.Name.Contains("Sample", StringComparison.OrdinalIgnoreCase)
                            && !s.Name.Contains("build", StringComparison.OrdinalIgnoreCase));

            foreach (var project in projectsToPack)
            {
                var description = ExtractContentFromNuSpecFile(project, "description");
                var tags = ExtractContentFromNuSpecFile(project, "tags");

                DotNetPack(s => s
                    .SetProject(project)
                    .SetOutputDirectory(NugetOutputDirectory)
                    .SetNoBuild(true)
                    .SetNoRestore(true)
                    .SetConfiguration(Configuration)
                    .SetVersion(GitVersion.NuGetVersionV2)
                    .SetAuthors("Mehdi Okhovat")
                    .SetDescription(description)
                    .SetPackageTags(tags)
                    .SetPackageLicenseUrl("https://github.com/m-okhovat/liteserver/blob/master/LICENSE")
                    .SetPackageProjectUrl("https://github.com/m-okhovat/liteserver"));
            }
        });

    string ExtractContentFromNuSpecFile(Project project, string section)
    {
        var fileContent = GetFileContent($"{project.Directory }/{project.Name}.nuspec");

        var xml = new XmlDocument();
        xml.LoadXml(fileContent);

        var xnList = xml.SelectNodes("/package/metadata");

        foreach (XmlNode xn in xnList)
        {
            return xn[section]?.InnerText;

        }

        return string.Empty;
    }

    Target PushNugetPackages => _ => _
        .DependsOn(PackNugetPackages)
        .Executes(() =>
        {
            var nugetFiles = GlobFiles(NugetOutputDirectory, "*.nupkg");

            foreach (var file in nugetFiles)
            {
                DotNetNuGetPush(a => a
                        .SetApiKey(NugetApiKey)
                        .SetSource(NugetServerUrl)
                        .SetTargetPath(file));
            }
        });

    string GetFileContent(string filePath)
    {
        var content = File.ReadAllText(filePath, Encoding.UTF8);
        return content;
    }
}

