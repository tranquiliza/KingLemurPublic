using System;
using System.Collections.Generic;
using System.Text;

namespace KingLemurJulian.Core.Models
{
    public class Quote
    {
        public int Id { get; private set; }
        public string Channel { get; private set; }
        public string QuoteText { get; private set; }
        public DateTime CreationTime { get; private set; }
        public string CreatedBy { get; private set; }
        public bool Deleted { get; private set; }

        [Obsolete("Only for serialization", true)]
        public Quote() { }

        public Quote(string channel, string quoteText, DateTime creationTime, string createdBy)
        {
            Channel = channel;
            QuoteText = quoteText;
            CreationTime = creationTime;
            CreatedBy = createdBy;
            Deleted = false;
        }

        private Quote(int id, string channel, string quoteText, DateTime creationTime, string createdBy, bool deleted)
        {
            Id = id;
            Channel = channel;
            QuoteText = quoteText;
            CreationTime = creationTime;
            Deleted = deleted;
            CreatedBy = createdBy;
        }

        public static Quote CreateFromDatabase(int id, string channel, string quoteText, DateTime creationTime, string createdBy, bool deleted)
            => new Quote(id, channel, quoteText, creationTime, createdBy, deleted);

        public string GetFormattedQuote()
        {
            return $"Quote #{Id}: {QuoteText} {string.Format(new FancyDateTimeFormatProvider(), "{0}", CreationTime.Date)}";
        }
    }
}
