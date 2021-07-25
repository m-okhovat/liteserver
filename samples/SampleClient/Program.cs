using LiteServer.Extensions;
using LiteServer.Extensions.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace SampleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var host = Host.CreateDefaultBuilder(args)
                    .ConfigureLiteServerDefaults(builder =>
                        builder.UseEndpoints(routeBuilder =>
                        {

                        }), "http://localhost:5001/")
                    .Build();

                host.Run();


            }
            catch (TaskSchedulerException e)
            {

            }
            catch (AggregateException ex)
            {

            }

            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
    }
}

