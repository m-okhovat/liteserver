using LiteServer.Extensions;
using LiteServer.Extensions.Hosting;
using LiteServer.Http.Extensions;
using LiteServer.Listener.Handlers;
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
                    {
                        builder.Use(FirstMiddleware)
                                .Use(SecondMiddleware);

                        builder.UseEndpoints(routeBuilder =>
                        {
                            routeBuilder.Map("/Health", context => context.Response.WriteAsync("I am healthy"));
                        });
                    }, "http://localhost:5001/")
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
}

