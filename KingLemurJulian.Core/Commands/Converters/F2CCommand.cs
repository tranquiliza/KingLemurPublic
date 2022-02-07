using KingLemurJulian.Core.Events;
using KingLemurJulian.Core.Extensions;
using MediatR;
using System.Linq;
using System.Threading.Tasks;

namespace KingLemurJulian.Core.Commands
{
    public class F2CCommand : CommandExecutorBase
    {
        public override string CommandName => "F2C";

        private readonly IMediator mediator;

        public F2CCommand(IMediator mediator)
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

            var result = (number - 32) * 5 / 9;
            await mediator.Send(new CommandResponseRequest(commandRequest, $"{number}°F converts to {result.ToInvarientStringWith2Decimals()}°C")).ConfigureAwait(false);
        }
    }
}
