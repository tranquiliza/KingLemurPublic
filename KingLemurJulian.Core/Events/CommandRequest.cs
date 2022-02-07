using MediatR;
using System.Collections.Generic;

namespace KingLemurJulian.Core.Events
{
    public class CommandRequest : IRequest
    {
        public ChatMessageEvent ChatMessage { get; set; }
        public List<string> Arguments { get; set; }
        public string Argument { get; set; }
        public string CommandText { get; set; }
    }
}
