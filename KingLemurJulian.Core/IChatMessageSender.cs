namespace KingLemurJulian.Core
{
    public interface IChatMessageSender
    {
        void SendMessage(string channel, string message);
    }
}
