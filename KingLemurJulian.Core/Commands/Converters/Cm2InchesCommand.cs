using KingLemurJulian.Core.Events;
using KingLemurJulian.Core.Extensions;
using MediatR;
using System.Linq;
using System.Threading.Tasks;

namespace KingLemurJulian.Core.Commands
{
    public class Cm2InchesCommand : CommandExecutorBase
    {
        public override string CommandName => "Cm2Inches";

        private readonly IMediator mediator;

        public Cm2InchesCommand(IMediator mediator)
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

            var result = number / 2.54;
            await mediator.Send(new CommandResponseRequest(commandRequest, $"{number}CM converts to {result.ToInvarientStringWith2Decimals()} Inches")).ConfigureAwait(false);
        }
    }
}
