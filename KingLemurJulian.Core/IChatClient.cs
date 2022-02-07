using KingLemurJulian.Core.Events;
using System;
using System.Threading.Tasks;

namespace KingLemurJulian.Core
{
    public interface IChatClient : IDisposable
    {
        event Func<ChatMessageEvent, Task> OnMessageReceived;
        event Func<CommandRequest, Task> OnCommandReceived;

        void Initialize();
        Task ConnectAsync();
        void JoinChannel(string channelName);
        void LeaveChannel(string channelName);
    }
}
