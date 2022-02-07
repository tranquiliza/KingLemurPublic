using KingLemurJulian.Core.Events;
using KingLemurJulian.Core.Extensions;
using MediatR;
using System;
using System.Threading.Tasks;

namespace KingLemurJulian.Core.Commands
{
    public class Cm2FeetCommand : CommandExecutorBase
    {
        private readonly IMediator mediator;

        public Cm2FeetCommand(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public override string CommandName => "CM2FEET";

        public override async Task Execute(CommandRequest commandRequest)
        {
            var input = commandRequest.Argument;
            if (!double.TryParse(input, out var centimeters))
            {
                await mediator.Send(new CommandResponseRequest(commandRequest, "Please give me a number")).ConfigureAwait(false);
                return;
            }

            var inches = centimeters / 2.54;
            var feet = Math.Floor(inches / 12);
            var leftOverInches = Math.Round(inches % 12);

            await mediator.Send(new CommandResponseRequest(commandRequest, $"{commandRequest.Argument}cm converts to {feet}'{leftOverInches.ToInvarientStringWith2Decimals()}\"")).ConfigureAwait(false);
        }
    }
}
