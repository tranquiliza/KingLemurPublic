using KingLemurJulian.Core.Events;
using MediatR;
using System.Threading.Tasks;

namespace KingLemurJulian.Core
{
    public class BotRunner : IBotRunner
    {
        private readonly IChatClient chatClient;
        private readonly IChannelRepository channelRepository;
        private readonly IMediator mediator;

        public BotRunner(IChatClient chatClient, IChannelRepository channelRepository, IMediator mediator)
        {
            this.chatClient = chatClient;
            this.channelRepository = channelRepository;
            this.mediator = mediator;

            chatClient.OnCommandReceived += ChatClient_OnCommandReceived;
            chatClient.OnMessageReceived += ChatClient_OnMessageReceived;
        }

        private async Task ChatClient_OnMessageReceived(ChatMessageEvent chatMessageEvent)
        {
            await mediator.Publish(chatMessageEvent).ConfigureAwait(false);
        }

        private async Task ChatClient_OnCommandReceived(CommandRequest commandRequest)
        {
            await mediator.Send(commandRequest).ConfigureAwait(false);
        }

        public async Task InitializeAsync()
        {
            chatClient.Initialize();
            await chatClient.ConnectAsync().ConfigureAwait(false);
        }

        public async Task JoinChannels()
        {
            foreach (var channelName in await channelRepository.GetChannelsToJoin().ConfigureAwait(false))
                chatClient.JoinChannel(channelName);
        }

        public void Dispose()
        {
            chatClient.Dispose();
        }
    }
}
