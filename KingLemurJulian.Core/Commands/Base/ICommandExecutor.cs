using KingLemurJulian.Core.Events;
using System.Threading.Tasks;

namespace KingLemurJulian.Core.Commands
{
    public interface ICommandExecutor
    {
        string CommandName { get; }
        bool CanExecute(CommandRequest commandRequest);
        Task Execute(CommandRequest commandRequest);
    }
}
