using KingLemurJulian.Core.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KingLemurJulian.Core.Commands
{
    public class SquidCommand : CommandExecutorBase
    {
        public override string CommandName => "Squid";

        private readonly IMediator mediator;
        private readonly Random rng;

        public SquidCommand(IMediator mediator)
        {
            this.mediator = mediator;
            rng = new Random();
        }

        public override async Task Execute(CommandRequest commandRequest)
        {
            int.TryParse(commandRequest.Argument, out int iterations);
            var squidEmotes = new List<string> {
                "Squid1",
                "Squid2",
                "Squid3",
                "Squid4"
            };

            var guardedIterations = iterations == 0 ? 10 : iterations;
            if (guardedIterations > 61)
                guardedIterations = 61;

            var builder = new StringBuilder();
            for (int i = 0; i < guardedIterations; i++)
            {
                var emoteToUse = rng.Next(0, squidEmotes.Count);
                builder.Append(squidEmotes[emoteToUse]).Append(" ");
            }

            await mediator.Send(new CommandResponseRequest(commandRequest, builder.ToString())).ConfigureAwait(false);
        }
    }
}
