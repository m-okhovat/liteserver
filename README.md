LiteServer is a lightweight HTTP web server that can be used instead of Kestrel. It can be used in console .net core applications and it is possible to add endpoints to console applications using the LiteServer. also it is possible to add middlewares in the pipeline to process the Http request.

The library targets .NET Standard 2.0 which supports the following runtimes (and newer):

* .Net Framework 4.6.1
* .NET Core 2.0

# Best practices and usage:

this library allows you to add a light-weight Http server and a custom pipeline and routing into your project. It has also some extensions that makes it easy to configure the LiteServer during the GenericHostBuilder configuration in the program class, in .net core projects.

# Installation

# Quick Start

After installing the library, you should:

#### .Net Framework projects:

1. Initialize the `LiteServerBuilder` and give it the customized configuration.
2. Create the LiteServer by calling the `Build` method on the `LiteServerBuilder`
3. Start the `LiteServer` by calling the `StartAsync` and start listening to http requests.

the following is a simple implementation that shows how to configure `LiteServer` in a .net framework console application.

``` csharp
 class Program
    {
        static void Main(string[] args)
        {
            var liteServerBuilder = new LiteServerBuilder();
            var pathConfigurations = new List<Action<BasePathBuilder>>()
            {
                builder => builder
                    .WithHostName("localhost")
                    .WithPort(5000)
            };

            var liteHost = liteServerBuilder.Configure(builder =>
                {
                    builder.Use(FirstMiddleware) //here we defined the middleware pipeline
                        .Use(SecondMiddleware);

                    builder.UseEndpoints(routeBuilder => // here we defined routing
                    {
                        routeBuilder.Map("/Hello", context => context.Response.WriteAsync("Hello world"));
                    });
                })
                .UseHttpListenerServer(pathConfigurations)
                .Build();

            liteHost.StartAsync();

            Console.ReadKey();

        }

        static HandlerDelegate FirstMiddleware(HandlerDelegate next)
        {
            return async context =>
            {
                await context.Response.WriteAsync("first middleware says hello! =>");
                await next(context);
            };
        }

        static HandlerDelegate SecondMiddleware(HandlerDelegate next)
        {
            return async context =>
            {
                await context.Response.WriteAsync("second middleware says hello =>");
                await next(context);
            };
        }
    }
```

#### .Net Core projects:

1. Create Microsoft Generic Host by calling the `CreateDefaultBuilder ` on `Host` class.
2. configure the `LiteServer` by using `ConfigureLiteServerDefaults` extension method on `IHostBuilder`.

the following is a simple implementation for .net core projects:

```csharp

 class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var host = Host.CreateDefaultBuilder(args)
                    .ConfigureLiteServerDefaults(builder =>
                    {
                        builder.Use(FirstMiddleware) //here we defined the middleware pipeline
                            .Use(SecondMiddleware);

                        builder.UseEndpoints(routeBuilder => // her we defined the routing and endpoints
                        {
                            routeBuilder.Map("/Health", context => context.Response.WriteAsync("I am healthy"));
                        });
                    }, "http://localhost:5001/")
                    .Build();

                host.Run();

            }

            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        static HandlerDelegate FirstMiddleware(HandlerDelegate next)
        {
            return async context =>
            {
                await context.Response.WriteAsync("first middleware says hello! =>");
                await next(context);
            };
        }

        static HandlerDelegate SecondMiddleware(HandlerDelegate next)
        {
            return async context =>
            {
                await context.Response.WriteAsync("second middleware says hello =>");
                await next(context);
            };
        }
    }
```







