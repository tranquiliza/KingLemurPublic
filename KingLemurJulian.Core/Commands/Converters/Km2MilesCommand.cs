using KingLemurJulian.Core.Events;
using KingLemurJulian.Core.Extensions;
using MediatR;
using System.Linq;
using System.Threading.Tasks;

namespace KingLemurJulian.Core.Commands
{
    public class Km2MilesCommand : CommandExecutorBase
    {
        public override string CommandName => "Km2Miles";

        private readonly IMediator mediator;

        public Km2MilesCommand(IMediator mediator)
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

            var result = number / 1.609;
            await mediator.Send(new CommandResponseRequest(commandRequest, $"{number} kilometers converts to {result.ToInvarientStringWith2Decimals()} miles")).ConfigureAwait(false);
        }
    }
}
