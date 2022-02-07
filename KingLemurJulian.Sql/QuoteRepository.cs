using KingLemurJulian.Core;
using KingLemurJulian.Core.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KingLemurJulian.Sql
{
    public class QuoteRepository : IQuoteRepository
    {
        private readonly ILogger<QuoteRepository> logger;
        private readonly ISqlAccess sql;

        public QuoteRepository(string connectionString, ILogger<QuoteRepository> logger)
        {
            sql = SqlAccessBase.Create(connectionString);
            this.logger = logger;
        }

        public async Task<Quote> GetQuoteAsync(int id)
        {
            try
            {
                using (var command = sql.CreateStoredProcedure("[Core].[GetQuote]"))
                {
                    command.WithParameter("id", id);

                    using (var reader = await command.ExecuteReaderAsync().ConfigureAwait(false))
                    {
                        if (await reader.ReadAsync().ConfigureAwait(false))
                        {
                            return Quote.CreateFromDatabase(
                                reader.GetInt32("id"),
                                reader.GetString("channel"),
                                reader.GetString("quoteText"),
                                reader.GetDateTime("creationTime"),
                                reader.GetString("createdBy"),
                                reader.GetBoolean("deleted"));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex, "Attempted to get quote with Id {arg} but failed", id);
                throw;
            }

            return null;
        }

        public async Task<List<Quote>> GetQuotesAsync()
        {
            var result = new List<Quote>();

            try
            {
                using (var command = sql.CreateStoredProcedure("[Core].[GetQuotes]"))
                using (var reader = await command.ExecuteReaderAsync().ConfigureAwait(false))
                {
                    while (await reader.ReadAsync().ConfigureAwait(false))
                    {
                        result.Add(Quote.CreateFromDatabase(
                            reader.GetInt32("id"),
                            reader.GetString("channel"),
                            reader.GetString("quoteText"),
                            reader.GetDateTime("creationTime"),
                            reader.GetString("createdBy"),
                            reader.GetBoolean("deleted")));
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex, "Failed to retrieve all quotes");
                throw;
            }

            return result;
        }

        public async Task<int> SaveAsync(Quote quote)
        {
            using (var command = sql.CreateStoredProcedure("[Core].[InsertQuote]"))
            {
                command.WithParameter("channel", quote.Channel)
                .WithParameter("quoteText", quote.QuoteText)
                .WithParameter("creationTime", quote.CreationTime)
                .WithParameter("createdby", quote.CreatedBy)
                .WithParameter("deleted", quote.Deleted);

                await command.ExecuteNonQueryAsync().ConfigureAwait(false);
            }

            using (var command = sql.CreateStoredProcedure("[Core].[GetLatestQuoteId]"))
            using (var reader = await command.ExecuteReaderAsync().ConfigureAwait(false))
            {
                if (await reader.ReadAsync().ConfigureAwait(false))
                    return reader.GetInt32("Id");
            }

            throw new InvalidOperationException("Was unable to return the Id of the latest quote");
        }
    }
}
