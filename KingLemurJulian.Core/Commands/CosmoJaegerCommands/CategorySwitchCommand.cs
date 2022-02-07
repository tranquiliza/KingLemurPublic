using System.Threading.Tasks;
using KingLemurJulian.Core.Commands;
using KingLemurJulian.Core.Events;
using MediatR;

namespace KingLemurJulian.Core.CosmoJaegerCommands
{
    public class CategorySwitchCommand : CommandExecutorBase
    {
        private readonly IMediator mediator;
        public override string CommandName => "c";

        public CategorySwitchCommand(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public override bool CanExecute(CommandRequest commandRequest)
        {
            if (commandRequest.ChatMessage.IsBroadcaster)
            {
                var intermediate = base.CanExecute(commandRequest);
                return intermediate;
            }

            return false;
        }

        public override async Task Execute(CommandRequest commandRequest)
        {
            var command = commandRequest.Argument.ToLower();

            var response = "!category ";

            switch (command)
            {
                case "jc":
                    response += "Just Chatting";
                    break;
                case "to":
                    response += "Travel & Outdoors";
                    break;
                case "special":
                    response += "Special Events";
                    break;
                case "beach":
                    response += "Pools, Hot Tubs, and Beaches";
                    break;

                default:
                    response = "I do not know that shortcut";
                    break;
            }

            await mediator.Send(new CommandResponseRequest(commandRequest, response));
        }
    }
}