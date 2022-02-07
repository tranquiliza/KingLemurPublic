using KingLemurJulian.Core.Events;
using KingLemurJulian.Core.Extensions;
using MediatR;
using System.Linq;
using System.Threading.Tasks;

namespace KingLemurJulian.Core.Commands
{
    public class Mil2KmCommand : CommandExecutorBase
    {
        public override string CommandName => "Mil2Km";

        private readonly IMediator mediator;

        public Mil2KmCommand(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public override async Task Execute(CommandRequest commandRequest)
        {
            if (!double.TryParse(commandRequest.Arguments.FirstOrDefault(), out var number))
            {
                await mediator.Send(new CommandResponseRequest(commandRequest, "Please give me a number")).ConfigureAwait(false);
            }

            var result = number * 10;
            await mediator.Send(new CommandResponseRequest(commandRequest, $"{number} norwegian mil converts to {result.ToInvarientStringWith2Decimals()} kilometers")).ConfigureAwait(false);
        }
    }
}
