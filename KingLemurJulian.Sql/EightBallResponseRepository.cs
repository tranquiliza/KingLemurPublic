using KingLemurJulian.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KingLemurJulian.Sql
{
    public class EightBallResponseRepository : IEightBallResponseRepository
    {
        private readonly ILogger<EightBallResponseRepository> logger;
        private readonly ISqlAccess sql;

        public EightBallResponseRepository(string connectionString, ILogger<EightBallResponseRepository> logger)
        {
            this.logger = logger;
            sql = SqlAccessBase.Create(connectionString);
        }

        public async Task<List<string>> GetBallResponses()
        {
            var result = new List<string>();

            try
            {
                using (var command = sql.CreateStoredProcedure("[Core].[GetEightBallResponses]"))
                using (var reader = await command.ExecuteReaderAsync().ConfigureAwait(false))
                {
                    while (await reader.ReadAsync().ConfigureAwait(false))
                    {
                        result.Add(reader.GetString("response"));
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex, "Unable to get eightball responses");
                throw;
            }

            return result;
        }

        public async Task<List<string>> GetIAmListeningResponses()
        {
            var result = new List<string>();

            try
            {
                using (var command = sql.CreateStoredProcedure("[Core].[GetIamListeningResponses]"))
                using (var reader = await command.ExecuteReaderAsync().ConfigureAwait(false))
                {
                    while (await reader.ReadAsync().ConfigureAwait(false))
                    {
                        result.Add(reader.GetString("response"));
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex, "Was unable to get listening responses");
                throw;
            }

            return result;
        }
    }
}
