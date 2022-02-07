using KingLemurJulian.Core.Events;
using System;
using System.Threading.Tasks;

namespace KingLemurJulian.Core.Commands
{
    public abstract class CommandExecutorBase : ICommandExecutor
    {
        public abstract string CommandName { get; }

        public virtual bool CanExecute(CommandRequest commandRequest)
           => string.Equals(commandRequest.CommandText, CommandName, StringComparison.OrdinalIgnoreCase);

        public abstract Task Execute(CommandRequest commandRequest);
    }
}
