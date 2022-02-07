using KingLemurJulian.Core.Events;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KingLemurJulian.Core.Handlers
{
    public class ChromaSpecialChatHandler : INotificationHandler<ChatMessageEvent>
    {
        private readonly IMediator mediator;

        public ChromaSpecialChatHandler(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task Handle(ChatMessageEvent message, CancellationToken cancellationToken)
        {
            if (!string.Equals(message.Username, "chromacarina", StringComparison.OrdinalIgnoreCase))
                return;

            if (message.Message.IndexOf("thanks julian", StringComparison.OrdinalIgnoreCase) >= 0)
                await mediator.Send(new ChatResponseRequest(message, "I love you too Chroma... BibleThump")).ConfigureAwait(false);
        }
    }
}
