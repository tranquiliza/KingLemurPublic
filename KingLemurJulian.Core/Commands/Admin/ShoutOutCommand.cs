using KingLemurJulian.Core.Events;
using MediatR;
using System;
using System.Threading.Tasks;

namespace KingLemurJulian.Core.Commands
{
    public class ShoutOutCommand : CommandExecutorBase
    {
        public override string CommandName => "SO";

        private readonly IMediator mediator;

        public ShoutOutCommand(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public override bool CanExecute(CommandRequest commandRequest)
        {
            if (commandRequest.ChatMessage.IsBroadcaster)
                return BaseExecuteWithExtraName();

            if (commandRequest.ChatMessage.IsModerator)
                return BaseExecuteWithExtraName();

            if (commandRequest.ChatMessage.IsVip)
                return BaseExecuteWithExtraName();

            return false;

            bool BaseExecuteWithExtraName() => base.CanExecute(commandRequest) || string.Equals("shoutout", commandRequest.CommandText, StringComparison.OrdinalIgnoreCase);
        }

        public override Task Execute(CommandRequest commandRequest)
        {
            return mediator.Send(new CommandResponseRequest(commandRequest, $"If you havn't checked out this user already, what are you doing ?! https://twitch.tv/{commandRequest.Argument}/"));
        }
    }
}
