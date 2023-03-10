using Microsoft.Extensions.Configuration;
using TaskManager.Buissines.Sevices;
using Unity;
using Unity.Lifetime;
using Unity.Microsoft.DependencyInjection;

namespace TaskManager.Presentation.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .UseUnityServiceProvider()
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();

            });
    }

}
