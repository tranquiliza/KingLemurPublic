using KingLemurJulian.Core;
using KingLemurJulian.Sql;
using KingLemurJulian.TwitchIntegration;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Reflection;

namespace KingLemurJulian.BackgroundHost
{
    public static class DependencyInjection
    {
        public static void ConfigureDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddLogging(builder =>
            {
                var environmentName = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");
                if (string.Equals(environmentName, "development", StringComparison.OrdinalIgnoreCase))
                    builder.AddConsole();
                else
                    builder.AddSeq(configuration.GetSection("Seq"));
            });

            var twitchChatSettings = new TwitchClientSettings(configuration.GetRequiredValue<string>("Twitch:Username"), configuration.GetRequiredValue<string>("Twitch:OAuth"));

            var connectionString = configuration.GetRequiredValue<string>("ConnectionStrings:SqlDatabase");

            services.RegisterBotCommands();

            services.AddSingleton<IBotRunner, BotRunner>();
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
            services.AddSingleton<IChannelRepository, ChannelRepository>(serviceProvider => new ChannelRepository(connectionString, serviceProvider.GetRequiredService<ILogger<ChannelRepository>>()));
            services.AddSingleton<IQuoteRepository, QuoteRepository>(serviceProvider => new QuoteRepository(connectionString, serviceProvider.GetRequiredService<ILogger<QuoteRepository>>()));
            services.AddSingleton<IDynamicCommandRepository, DynamicCommandRepository>(serviceProvider => new DynamicCommandRepository(connectionString, serviceProvider.GetRequiredService<ILogger<DynamicCommandRepository>>()));
            services.AddSingleton<IEightBallResponseRepository, EightBallResponseRepository>(serviceProvider => new EightBallResponseRepository(connectionString, serviceProvider.GetRequiredService<ILogger<EightBallResponseRepository>>()));
            services.AddSingleton(_ => new HttpClient());
            services.AddHostedService<BotService>();

            services.AddMediatR(typeof(DependencyInjection).GetTypeInfo().Assembly, typeof(IBotRunner).GetTypeInfo().Assembly);

            var container = services.BuildServiceProvider();

            var twitchChatClient = new TwitchChatClient(twitchChatSettings, container.GetRequiredService<ILogger<TwitchChatClient>>());
            services.AddSingleton<IChatClient>(twitchChatClient);
            services.AddSingleton<IChatMessageSender>(twitchChatClient);
        }
    }
}
