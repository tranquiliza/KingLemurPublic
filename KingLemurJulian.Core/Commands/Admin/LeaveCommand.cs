using KingLemurJulian.Core.Events;
using MediatR;
using System;
using System.Threading.Tasks;

namespace KingLemurJulian.Core.Commands
{
    public class LeaveCommand : CommandExecutorBase
    {
        private readonly IChatClient chatClient;
        private readonly IChannelRepository channelRepository;
        private readonly IMediator mediator;

        public LeaveCommand(IChatClient chatClient, IChannelRepository channelRepository, IMediator mediator)
        {
            this.chatClient = chatClient;
            this.channelRepository = channelRepository;
            this.mediator = mediator;
        }

        public override string CommandName => "Leave";

        public override bool CanExecute(CommandRequest commandRequest)
        {
            if (commandRequest.ChatMessage.IsBroadcaster)
                return base.CanExecute(commandRequest);

            if (string.Equals("cosmojaeger", commandRequest.ChatMessage.Username, StringComparison.OrdinalIgnoreCase))
                return base.CanExecute(commandRequest);

            if (string.Equals("chromacarina", commandRequest.ChatMessage.Username, StringComparison.OrdinalIgnoreCase))
                return base.CanExecute(commandRequest);

            return false;
        }

        public override async Task Execute(CommandRequest command)
        {
            var channelName = command.Argument;
            if (string.IsNullOrEmpty(channelName))
                channelName = command.ChatMessage.Channel;

            var userExecuting = command.ChatMessage.Username;

            // If trying to leave a channel, from another channel.
            if (!string.Equals(userExecuting, command.CommandText, StringComparison.OrdinalIgnoreCase) && !string.Equals(userExecuting, "cosmojaeger", StringComparison.OrdinalIgnoreCase))
                return;

            await mediator.Send(new CommandResponseRequest(command, $"Leaving {channelName}'s Channel. Goodbye!")).ConfigureAwait(false);

            chatClient.LeaveChannel(channelName);

            await channelRepository.DeleteChannel(channelName).ConfigureAwait(false);
        }
    }
}
