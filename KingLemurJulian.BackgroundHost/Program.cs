using Microsoft.Extensions.Hosting;

namespace KingLemurJulian.BackgroundHost
{
    public static class Program
    {
        public static void Main(string[] args) => CreateHostBuilder(args).Build().Run();

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) => services.ConfigureDependencies(hostContext.Configuration));
    }
}
