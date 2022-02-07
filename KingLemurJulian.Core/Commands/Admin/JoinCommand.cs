using KingLemurJulian.Core.Events;
using MediatR;
using System;
using System.Threading.Tasks;

namespace KingLemurJulian.Core.Commands
{
    public class JoinCommand : CommandExecutorBase
    {
        private readonly IMediator mediator;
        private readonly IChatClient chatClient;
        private readonly IChannelRepository channelRepository;

        public override string CommandName => "Join";

        public JoinCommand(IMediator mediator, IChatClient chatClient, IChannelRepository channelRepository)
        {
            this.mediator = mediator;
            this.chatClient = chatClient;
            this.channelRepository = channelRepository;
        }

        public override bool CanExecute(CommandRequest commandRequest)
        {
            if (string.Equals("cosmojaeger", commandRequest.ChatMessage.Username, StringComparison.OrdinalIgnoreCase))
                return base.CanExecute(commandRequest);

            if (string.Equals("chromacarina", commandRequest.ChatMessage.Username, StringComparison.OrdinalIgnoreCase))
                return base.CanExecute(commandRequest);

            return false;
        }

        public override async Task Execute(CommandRequest commandRequest)
        {
            var channelToJoin = commandRequest.Argument;
            chatClient.JoinChannel(channelToJoin);

            await channelRepository.SaveChannel(channelToJoin).ConfigureAwait(false);

            await mediator.Send(new CommandResponseRequest(commandRequest, "Joined channel " + commandRequest.Argument)).ConfigureAwait(false);
        }
    }
}
