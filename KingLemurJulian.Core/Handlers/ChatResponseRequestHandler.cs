using KingLemurJulian.Core.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace KingLemurJulian.Core.Handlers
{
    public class ChatResponseRequestHandler : IRequestHandler<ChatResponseRequest, Unit>
    {
        private static class LogFormat
        {
            internal static void LogResponse(ILogger logger, ChatResponseRequest chatResponseRequest)
            {
                logger.LogInformation(
                    "Replied to {user} in channel {channel}, with response: {response}",
                    chatResponseRequest.ChatMessageEvent.DisplayName,
                    chatResponseRequest.Channel,
                    chatResponseRequest.Response);
            }
        }

        private readonly ILogger<ChatResponseRequestHandler> logger;
        private readonly IChatMessageSender chatMessageSender;

        public ChatResponseRequestHandler(ILogger<ChatResponseRequestHandler> logger, IChatMessageSender chatMessageSender)
        {
            this.logger = logger;
            this.chatMessageSender = chatMessageSender;
        }

        public Task<Unit> Handle(ChatResponseRequest request, CancellationToken cancellationToken)
        {
            LogFormat.LogResponse(logger, request);

            chatMessageSender.SendMessage(request.Channel, request.Response);

            return Unit.Task;
        }
    }
}
