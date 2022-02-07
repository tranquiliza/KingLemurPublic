using KingLemurJulian.Core.Events;
using MediatR;
using System;
using System.Threading.Tasks;

namespace KingLemurJulian.Core.Commands
{
    public class NewYearCommand : CommandExecutorBase
    {
        private readonly IMediator mediator;
        private readonly IDateTimeProvider dateTimeProvider;

        public NewYearCommand(IMediator mediator, IDateTimeProvider dateTimeProvider)
        {
            this.mediator = mediator;
            this.dateTimeProvider = dateTimeProvider;
        }

        public override string CommandName => "NewYear";

        public override async Task Execute(CommandRequest commandRequest)
        {
            var now = dateTimeProvider.Now;

            var target = new DateTime(now.Year, 12, 31, 23, 59, 59);

            var timeUntil = target - now;

            await mediator.Send(new CommandResponseRequest(commandRequest,
                $"Time until new year: {timeUntil.Days} days, {timeUntil.Hours} hours {timeUntil.Minutes} minutes and {timeUntil.Seconds} seconds")).ConfigureAwait(false);
        }
    }
}