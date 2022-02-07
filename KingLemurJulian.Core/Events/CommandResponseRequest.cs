using MediatR;

namespace KingLemurJulian.Core.Events
{
    public class CommandResponseRequest : IRequest
    {
        public CommandRequest CommandRequest { get; }
        public string Channel { get; }
        public string Response { get; }

        public CommandResponseRequest(CommandRequest commandRequest, string response)
        {
            CommandRequest = commandRequest;
            Channel = commandRequest.ChatMessage.Channel;
            Response = response;
        }
    }
}
