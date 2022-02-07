using KingLemurJulian.Core.Events;
using KingLemurJulian.Core.Extensions;
using MediatR;
using System.Threading.Tasks;

namespace KingLemurJulian.Core.Commands
{
    public class Feet2CmCommand : CommandExecutorBase
    {
        public override string CommandName => "Feet2Cm";

        private readonly IMediator mediator;

        public Feet2CmCommand(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public override async Task Execute(CommandRequest commandRequest)
        {
            var inputs = commandRequest.Argument;
            var split = inputs.Split('\'');
            if (split.Length < 1)
            {
                await mediator.Send(new CommandResponseRequest(commandRequest, "Please give me a number")).ConfigureAwait(false);
                return;
            }

            int.TryParse(split[0], out var feet);

            var inchesString = split[1];
            var inches = 0;
            if (split.Length == 2 && !int.TryParse(inchesString, out inches))
            {
                split[1].Remove(inchesString.Length - 1, 1);
                int.TryParse(inchesString, out inches);
            }

            var result = ((feet * 12) + inches) * 2.54;
            await mediator.Send(new CommandResponseRequest(commandRequest, $"{commandRequest.Argument} converts to {result.ToInvarientStringWith2Decimals()} CM")).ConfigureAwait(false);
        }
    }
}
