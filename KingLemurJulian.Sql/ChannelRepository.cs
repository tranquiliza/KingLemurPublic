using KingLemurJulian.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KingLemurJulian.Sql
{
    public class ChannelRepository : IChannelRepository
    {
        private readonly ISqlAccess sql;
        private readonly ILogger<ChannelRepository> logger;

        public ChannelRepository(string connectionString, ILogger<ChannelRepository> logger)
        {
            this.logger = logger;
            sql = SqlAccessBase.Create(connectionString);
        }

        public async Task<List<string>> GetChannelsToJoin()
        {
            var result = new List<string>();

            try
            {
                using (var command = sql.CreateStoredProcedure("[Core].[GetChannels]"))
                using (var reader = await command.ExecuteReaderAsync().ConfigureAwait(false))
                {
                    while (await reader.ReadAsync().ConfigureAwait(false))
                    {
                        result.Add(reader.GetString("Channel"));
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Was unable to get channels");
                throw;
            }

            return result;
        }

        public async Task DeleteChannel(string channelName)
        {
            try
            {
                using (var command = sql.CreateStoredProcedure("[Core].[DeleteChannel]"))
                {
                    command.WithParameter("channel", channelName);
                    await command.ExecuteNonQueryAsync().ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Was unable to delete channel: {channel}", channelName);
                throw;
            }
        }

        public async Task SaveChannel(string channelName)
        {
            try
            {
                using (var command = sql.CreateStoredProcedure("[Core].[UpsertChannel]"))
                {
                    command.WithParameter("channel", channelName);
                    await command.ExecuteNonQueryAsync().ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Was unable to save channel: {channel}", channelName);
                throw;
            }
        }
    }
}
