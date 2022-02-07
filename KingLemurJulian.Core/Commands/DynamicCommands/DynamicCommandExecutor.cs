using KingLemurJulian.Core.Events;
using MediatR;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace KingLemurJulian.Core.Commands
{
    public class DynamicCommandExecutor : CommandExecutorBase
    {
        public override string CommandName => "DYNAMIC";

        private readonly IDynamicCommandRepository dynamicCommandRepository;
        private readonly IMediator mediator;

        public DynamicCommandExecutor(IDynamicCommandRepository dynamicCommandRepository, IMediator mediator)
        {
            this.dynamicCommandRepository = dynamicCommandRepository;
            this.mediator = mediator;
        }

        public override bool CanExecute(CommandRequest commandRequest)
        {
            var commandIdentifiers = dynamicCommandRepository.GetCommandIdentifiers();
            return commandIdentifiers.Count(x => string.Equals(x, commandRequest.CommandText, StringComparison.OrdinalIgnoreCase)) > 0;
        }

        public override async Task Execute(CommandRequest commandRequest)
        {
            var command = await dynamicCommandRepository.GetDynamicCommandAsync(commandRequest.CommandText).ConfigureAwait(false);

            var response = command.GetFormattedResponse(commandRequest.ChatMessage.DisplayName, commandRequest.Argument);

            await mediator.Send(new CommandResponseRequest(commandRequest, response)).ConfigureAwait(false);
        }
    }
}
