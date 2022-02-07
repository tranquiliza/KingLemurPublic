using KingLemurJulian.Core.Events;
using KingLemurJulian.Core.Extensions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingLemurJulian.Core.Commands
{
    public class Pound2KiloCommand : CommandExecutorBase
    {
        public override string CommandName => "Pound2Kilo";

        private readonly IMediator mediator;

        public Pound2KiloCommand(IMediator mediator)
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

            var result = number / 2.205;
            await mediator.Send(new CommandResponseRequest(commandRequest, $"{number} pounds converts to {result.ToInvarientStringWith2Decimals()} kilograms")).ConfigureAwait(false);
        }
    }
}
