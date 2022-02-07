using KingLemurJulian.Core.Events;
using MediatR;
using System;
using System.Text;
using System.Threading.Tasks;

namespace KingLemurJulian.Core.Commands
{
    public class RollCommand : CommandExecutorBase
    {
        public override string CommandName => "Roll";

        private readonly IMediator mediator;
        private readonly Random rng;

        public RollCommand(IMediator mediator)
        {
            this.mediator = mediator;
            rng = new Random();
        }

        public override async Task Execute(CommandRequest commandRequest)
        {
            var diceString = "1d20";

            if (!string.IsNullOrEmpty(commandRequest.Argument))
                diceString = commandRequest.Argument;

            var responseBuilder = new StringBuilder();

            responseBuilder.Append("You rolled: ");

            var total = 0;

            var multipleDice = false;

            var diceInstructions = diceString.Split('+');
            if (diceInstructions.Length > 1)
                multipleDice = true;

            foreach (var diceInstruction in diceInstructions)
            {
                var commands = diceInstruction.Split('d', 'D');
                if (commands.Length != 2)
                    continue;

                int.TryParse(commands[0], out var amountToRoll);
                int.TryParse(commands[1], out var diceSize);

                if (amountToRoll > 1)
                    multipleDice = true;

                responseBuilder.Append(multipleDice ? $"D{diceSize}s: (" : $"D{diceSize}: ");

                for (var i = 0; i < amountToRoll; i++)
                {
                    var result = rng.Next(1, diceSize + 1);
                    responseBuilder.Append($"{result}");
                    if (i + 1 < amountToRoll)
                        responseBuilder.Append("|");

                    total += result;
                }

                responseBuilder.Append(multipleDice ? ") " : " ");
            }

            if (multipleDice)
                responseBuilder.Append($"for a total of {total}.");


            await mediator.Send(new CommandResponseRequest(commandRequest, responseBuilder.ToString()));
        }
    }
}