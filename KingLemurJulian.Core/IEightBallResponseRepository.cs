using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KingLemurJulian.Core
{
    public interface IEightBallResponseRepository
    {
        Task<List<string>> GetBallResponses();
        Task<List<string>> GetIAmListeningResponses();
    }
}
