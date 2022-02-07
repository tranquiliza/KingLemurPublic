using KingLemurJulian.Core.Events;
using MediatR;
using System.Threading.Tasks;

namespace KingLemurJulian.Core.Commands
{
    public class QuoteListCommand : CommandExecutorBase
    {
        public override string CommandName => "quotelist";

        private readonly IMediator mediator;

        public QuoteListCommand(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public override async Task Execute(CommandRequest commandRequest)
        {
            await mediator.Send(new CommandResponseRequest(commandRequest, "Still to be implemented")).ConfigureAwait(false);
        }
    }
}
