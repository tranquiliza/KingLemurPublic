using KingLemurJulian.Core.Events;
using KingLemurJulian.Core.Models;
using MediatR;
using System;
using System.Text;
using System.Threading.Tasks;

namespace KingLemurJulian.Core.Commands
{
    public class AddCommand : CommandExecutorBase
    {
        public override string CommandName => "AddCommand";

        private readonly IMediator mediator;
        private readonly IDynamicCommandRepository dynamicCommandRepository;

        public AddCommand(IMediator mediator, IDynamicCommandRepository dynamicCommandRepository)
        {
            this.mediator = mediator;
            this.dynamicCommandRepository = dynamicCommandRepository;
        }

        public override bool CanExecute(CommandRequest commandRequest)
        {
            if (!string.Equals("cosmojaeger", commandRequest.ChatMessage.Username, StringComparison.OrdinalIgnoreCase))
                return false;

            return base.CanExecute(commandRequest);
        }

        public override async Task Execute(CommandRequest commandRequest)
        {
            if (commandRequest.Arguments.Count < 2)
            {
                await mediator.Send(new CommandResponseRequest(commandRequest, "Unable to create command, lack of arguments")).ConfigureAwait(false);
                return;
            }

            var stringBuilder = new StringBuilder();
            for (int i = 1; i < commandRequest.Arguments.Count; i++)
            {
                stringBuilder.Append(commandRequest.Arguments[i]).Append(" ");
            }

            var dynamicCommand = new DynamicCommand(commandRequest.Arguments[0], stringBuilder.ToString());
            await dynamicCommandRepository.SaveDynamicCommandAsync(dynamicCommand).ConfigureAwait(false);

            await mediator.Send(new CommandResponseRequest(commandRequest, $"Successfully created command ${dynamicCommand.CommandIdentifier}")).ConfigureAwait(false);
        }
    }
}
