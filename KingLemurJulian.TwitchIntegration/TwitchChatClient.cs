using KingLemurJulian.Core;
using KingLemurJulian.Core.Events;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;

namespace KingLemurJulian.TwitchIntegration
{
    public class TwitchChatClient : IChatClient, IChatMessageSender
    {
        private readonly TwitchClientSettings twitchClientSettings;
        private readonly ILogger<TwitchChatClient> logger;
        private readonly TwitchClient client;

        public event Func<ChatMessageEvent, Task> OnMessageReceived;
        public event Func<CommandRequest, Task> OnCommandReceived;

        public TwitchChatClient(TwitchClientSettings twitchClientSettings, ILogger<TwitchChatClient> logger)
        {
            this.twitchClientSettings = twitchClientSettings;
            this.logger = logger;
            client = new TwitchClient();
            client.OnMessageReceived += Client_OnMessageReceived;
            client.OnChatCommandReceived += Client_OnChatCommandReceived;
        }

        private void Client_OnChatCommandReceived(object sender, OnChatCommandReceivedArgs e)
        {
            logger.LogInformation("Received command {arg} from channel {channel}, by user {user} ", e.Command.CommandText, e.Command.ChatMessage.Channel, e.Command.ChatMessage.DisplayName);
            OnCommandReceived?.Invoke(e.Command.MapToCommandRequest());
        }

        private void Client_OnMessageReceived(object sender, OnMessageReceivedArgs e)
        {
            OnMessageReceived?.Invoke(e.ChatMessage.MapToChatMessageEvent());
        }

        public void Initialize()
        {
            var credentials = new ConnectionCredentials(twitchClientSettings.TwitchUsername, twitchClientSettings.TwitchBotOAuth);
            client.Initialize(credentials, chatCommandIdentifier: '$');
            logger.LogInformation("Twitch chat client initialized");
        }

        public void JoinChannel(string channelName)
        {
            var clientHasAlreadyJoined = client.JoinedChannels.Any(x => string.Equals(x.Channel, channelName, StringComparison.OrdinalIgnoreCase));
            if (clientHasAlreadyJoined)
                return;

            client.JoinChannel(channelName);

            logger.LogInformation($"Joined channel: {channelName}");
        }

        public void LeaveChannel(string channelName)
        {
            client.LeaveChannel(channelName);

            logger.LogInformation($"Leaving channel: {channelName}");
        }

        public void SendMessage(string channel, string message)
        {
            if (!client.JoinedChannels.Any(x => string.Equals(x.Channel, channel, StringComparison.OrdinalIgnoreCase)))
            {
                logger.LogWarning("Was unable to send message to {arg}'s channel, the client is no longer connected?", channel);
                return;
            }

            client.SendMessage(channel, message);
        }

        private TaskCompletionSource<bool> connectionCompletionTask = new TaskCompletionSource<bool>();
        public async Task ConnectAsync()
        {
            client.OnConnected += TwitchClient_OnConnected;
            client.Connect();

            await connectionCompletionTask.Task.ConfigureAwait(false);
            logger.LogInformation("Twitch chat client connected");
        }

        private void TwitchClient_OnConnected(object sender, OnConnectedArgs e)
        {
            client.OnConnected -= TwitchClient_OnConnected;

            connectionCompletionTask.SetResult(true);
            connectionCompletionTask = new TaskCompletionSource<bool>();
        }

        public void Dispose()
        {
            client.Disconnect();
            logger.LogInformation("Twitch client disconnect sent");
        }
    }
}
