using KingLemurJulian.Core.Events;
using TwitchLib.Client.Models;

namespace KingLemurJulian.TwitchIntegration
{
    public static class EventMapper
    {
        public static ChatMessageEvent MapToChatMessageEvent(this ChatMessage chatMessage)
        {
            return new ChatMessageEvent
            {
                ColorHex = chatMessage.ColorHex,
                Username = chatMessage.Username,
                DisplayName = chatMessage.DisplayName,
                IsHighlighted = chatMessage.IsHighlighted,
                IsVip = chatMessage.IsVip,
                IsSubscriber = chatMessage.IsSubscriber,
                IsSkippingSubMode = chatMessage.IsSkippingSubMode,
                IsModerator = chatMessage.IsModerator,
                IsMe = chatMessage.IsMe,
                IsBroadcaster = chatMessage.IsBroadcaster,
                SubscribedMonthCount = chatMessage.SubscribedMonthCount,
                Channel = chatMessage.Channel,
                Message = chatMessage.Message
            };
        }

        public static CommandRequest MapToCommandRequest(this ChatCommand command)
        {
            return new CommandRequest
            {
                Argument = command.ArgumentsAsString,
                Arguments = command.ArgumentsAsList,
                CommandText = command.CommandText,
                ChatMessage = MapToChatMessageEvent(command.ChatMessage)
            };
        }
    }
}
