using System;
using System.Threading.Tasks;
using KingLemurJulian.Core.Commands;
using KingLemurJulian.Core.Events;
using MediatR;

namespace KingLemurJulian.Core.CosmoJaegerCommands
{
    public class TimedSceneSwitchCommand : CommandExecutorBase
    {
        private readonly IMediator mediator;
        public override string CommandName => "ts";

        public TimedSceneSwitchCommand(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public override bool CanExecute(CommandRequest commandRequest)
        {
            if (commandRequest.ChatMessage.IsBroadcaster)
                 return base.CanExecute(commandRequest);

            return false;
        }

        public override async Task Execute(CommandRequest commandRequest)
        {
            var input = commandRequest.Argument;

            if (!int.TryParse(input, out var seconds))
                return;

            await mediator.Send(new CommandResponseRequest(commandRequest, "!ss movie"));

            var switchBackDelay = TimeSpan.FromSeconds(seconds);

            await Task.Delay(switchBackDelay);
            await mediator.Send(new CommandResponseRequest(commandRequest, "!live"));
        }
    }
}