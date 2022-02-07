using KingLemurJulian.Core.Events;
using KingLemurJulian.Core.Models;
using MediatR;
using System;
using System.Threading.Tasks;

namespace KingLemurJulian.Core.Commands
{
    public class AddQuoteCommand : CommandExecutorBase
    {
        public override string CommandName => "QuoteAdd";

        private readonly IMediator mediator;
        private readonly IQuoteRepository quoteRepository;
        private readonly IDateTimeProvider dateTimeProvider;

        public AddQuoteCommand(IMediator mediator, IQuoteRepository quoteRepository, IDateTimeProvider dateTimeProvider)
        {
            this.mediator = mediator;
            this.quoteRepository = quoteRepository;
            this.dateTimeProvider = dateTimeProvider;
        }

        public override bool CanExecute(CommandRequest commandRequest)
        {
            if (commandRequest.ChatMessage.IsBroadcaster)
                return ExtendedBaseCanExecute();

            if (commandRequest.ChatMessage.IsModerator)
                return ExtendedBaseCanExecute();

            if (commandRequest.ChatMessage.IsVip)
                return ExtendedBaseCanExecute();

            if (string.Equals("cosmojaeger", commandRequest.ChatMessage.Username, StringComparison.OrdinalIgnoreCase))
                return ExtendedBaseCanExecute();

            return false;

            bool ExtendedBaseCanExecute() => base.CanExecute(commandRequest) || string.Equals(commandRequest.CommandText, "addQuote", StringComparison.OrdinalIgnoreCase);
        }

        public override async Task Execute(CommandRequest commandRequest)
        {
            var quote = new Quote(commandRequest.ChatMessage.Channel, commandRequest.Argument, dateTimeProvider.Now, commandRequest.ChatMessage.DisplayName);
            var newQuoteId = await quoteRepository.SaveAsync(quote).ConfigureAwait(false);
            await mediator.Send(new CommandResponseRequest(commandRequest, $"Quote #{newQuoteId} added successfully!")).ConfigureAwait(false);
        }
    }
}
