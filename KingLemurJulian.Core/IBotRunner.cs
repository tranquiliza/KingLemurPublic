using System;
using System.Threading.Tasks;

namespace KingLemurJulian.Core
{
    public interface IBotRunner : IDisposable
    {
        Task InitializeAsync();
        Task JoinChannels();
    }
}
