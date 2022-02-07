using KingLemurJulian.Core;
using KingLemurJulian.Core.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace KingLemurJulian.Sql
{
    public class DynamicCommandRepository : IDynamicCommandRepository
    {
        private readonly ISqlAccess sql;
        private readonly ILogger<DynamicCommandRepository> logger;

        public DynamicCommandRepository(string connectionString, ILogger<DynamicCommandRepository> logger)
        {
            sql = SqlAccessBase.Create(connectionString);
            this.logger = logger;
        }

        public async Task SaveDynamicCommandAsync(DynamicCommand dynamicCommand)
        {
            try
            {
                using (var command = sql.CreateStoredProcedure("[Core].[SaveDynamicCommand]"))
                {
                    command.WithParameter("commandIdentifer", dynamicCommand.CommandIdentifier)
                        .WithParameter("data", JsonSerializer.Serialize(dynamicCommand));

                    await command.ExecuteNonQueryAsync().ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex, "Unable to save dynamic command");
                throw;
            }
        }

        public async Task<DynamicCommand> GetDynamicCommandAsync(string identifier)
        {
            try
            {
                using (var command = sql.CreateStoredProcedure("[Core].[GetDynamicCommand]"))
                {
                    command.WithParameter("@commandIdentifier", identifier);

                    using (var reader = await command.ExecuteReaderAsync().ConfigureAwait(false))
                    {
                        if (await reader.ReadAsync().ConfigureAwait(false))
                        {
                            return JsonSerializer.Deserialize<DynamicCommand>(reader.GetString("data"));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex, "Unable to fetch Dynamic command from identifier");
                throw;
            }

            return null;
        }

        public async Task<List<string>> GetCommandIdentifiersAsync()
        {
            var identifiers = new List<string>();

            try
            {
                using (var command = sql.CreateStoredProcedure("[Core].[GetDynamicCommandIdentifiers]"))
                using (var reader = await command.ExecuteReaderAsync().ConfigureAwait(false))
                {
                    while (await reader.ReadAsync().ConfigureAwait(false))
                    {
                        identifiers.Add(reader.GetString("CommandIdentifier"));
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex, "Unable to get command identifiers");
                throw;
            }

            return identifiers;
        }

        public List<string> GetCommandIdentifiers()
        {
            var identifiers = new List<string>();

            try
            {
                using (var command = sql.CreateStoredProcedure("[Core].[GetDynamicCommandIdentifiers]"))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        identifiers.Add(reader.GetString("CommandIdentifier"));
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex, "Unable to get command identifiers");
                throw;
            }

            return identifiers;
        }
    }
}
