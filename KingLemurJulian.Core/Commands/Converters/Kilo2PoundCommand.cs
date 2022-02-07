using KingLemurJulian.Core.Events;
using KingLemurJulian.Core.Extensions;
using MediatR;
using System.Linq;
using System.Threading.Tasks;

namespace KingLemurJulian.Core.Commands
{
    public class Kilo2PoundCommand : CommandExecutorBase
    {
        public override string CommandName => "Kilo2Pound";

        private readonly IMediator mediator;

        public Kilo2PoundCommand(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public override async Task Execute(CommandRequest commandRequest)
        {
            if (!double.TryParse(commandRequest.Arguments.FirstOrDefault(), out var number))
            {
                await mediator.Send(new CommandResponseRequest(commandRequest, "Please give me a number")).ConfigureAwait(false);
                return;
            }

            var result = number * 2.205;
            await mediator.Send(new CommandResponseRequest(commandRequest, $"{number} kilograms converts to {result.ToInvarientStringWith2Decimals()} pounds")).ConfigureAwait(false);
        }
    }
}
