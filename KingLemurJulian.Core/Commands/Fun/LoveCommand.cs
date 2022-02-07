using KingLemurJulian.Core.Events;
using MediatR;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace KingLemurJulian.Core.Commands
{
    public class LoveCommand : CommandExecutorBase
    {
        public override string CommandName => "Love";

        private readonly IMediator mediator;
        private readonly Random rnd;

        public LoveCommand(IMediator mediator)
        {
            this.mediator = mediator;
            rnd = new Random();
        }

        public override async Task Execute(CommandRequest commandRequest)
        {
            var argumentWithoutAt = commandRequest.Argument.Trim('@');
            var displayName = commandRequest.ChatMessage.DisplayName;

            if (string.Equals(displayName, argumentWithoutAt, StringComparison.OrdinalIgnoreCase) || commandRequest.Argument.Length == 0)
            {
                await mediator.Send(new CommandResponseRequest(commandRequest, "Of cause you love yourself... Don't make me...")).ConfigureAwait(false);
                return;
            }

            if (string.Equals(argumentWithoutAt, "kingLemurJulian", StringComparison.OrdinalIgnoreCase))
            {
                await mediator.Send(new CommandResponseRequest(commandRequest, "You can't love a robot LUL ")).ConfigureAwait(false);
                return;
            }

            var roll = rnd.NextDouble();
            if (roll < 0.15)
            {
                await mediator.Send(new CommandResponseRequest(commandRequest, $"It's complicated between {displayName} and {argumentWithoutAt}")).ConfigureAwait(false);
                return;
            }
            else if (roll < 0.25)
            {
                await mediator.Send(new CommandResponseRequest(commandRequest, "Are you for real? LUL")).ConfigureAwait(false);
                return;
            }
            else if (roll < 0.35)
            {
                await mediator.Send(new CommandResponseRequest(commandRequest, $"{displayName} and {argumentWithoutAt} were meant for each other! <3")).ConfigureAwait(false);
                return;
            }

            var percent = rnd.NextDouble();
            await mediator.Send(new CommandResponseRequest(commandRequest, $"There is {Math.Round(percent * 100).ToString(CultureInfo.InvariantCulture)}% love between {displayName} and {argumentWithoutAt}")).ConfigureAwait(false);
        }
    }
}
