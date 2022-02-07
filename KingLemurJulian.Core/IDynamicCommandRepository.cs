using KingLemurJulian.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KingLemurJulian.Core
{
    public interface IDynamicCommandRepository
    {
        Task SaveDynamicCommandAsync(DynamicCommand dynamicCommand);
        Task<DynamicCommand> GetDynamicCommandAsync(string identifier);
        Task<List<string>> GetCommandIdentifiersAsync();
        List<string> GetCommandIdentifiers();
    }
}
