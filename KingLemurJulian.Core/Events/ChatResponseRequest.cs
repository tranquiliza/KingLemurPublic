using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace KingLemurJulian.Core.Events
{
    public class ChatResponseRequest : IRequest
    {
        public ChatMessageEvent ChatMessageEvent { get; }
        public string Response { get; }
        public string Channel { get; }

        public ChatResponseRequest(ChatMessageEvent chatMessageEvent, string response)
        {
            ChatMessageEvent = chatMessageEvent;
            Channel = chatMessageEvent.Channel;
            Response = response;
        }
    }
}
