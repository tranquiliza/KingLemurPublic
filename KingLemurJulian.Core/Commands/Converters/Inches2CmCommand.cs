using KingLemurJulian.Core.Events;
using KingLemurJulian.Core.Extensions;
using MediatR;
using System.Linq;
using System.Threading.Tasks;

namespace KingLemurJulian.Core.Commands
{
    public class Inches2CmCommand : CommandExecutorBase
    {
        public override string CommandName => "Inches2Cm";

        private readonly IMediator mediator;

        public Inches2CmCommand(IMediator mediator)
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

            var result = number * 2.54;
            await mediator.Send(new CommandResponseRequest(commandRequest, $"{number} Inches converts to {result.ToInvarientStringWith2Decimals()} Cm")).ConfigureAwait(false);
        }
    }
}
