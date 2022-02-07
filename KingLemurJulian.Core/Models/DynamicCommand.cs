using System;
using System.Text.Json.Serialization;

namespace KingLemurJulian.Core.Models
{
    public class DynamicCommand
    {
        private const string UsernameReplaceLabel = "[username]";
        private const string ArgumentReplaceLabel = "[argument]";

        [JsonInclude]
        [JsonPropertyName("id")]
        public int Id { get; private set; }

        [JsonInclude]
        [JsonPropertyName("command-identifier")]
        public string CommandIdentifier { get; private set; }

        [JsonInclude]
        [JsonPropertyName("command-response")]
        public string CommandResponse { get; private set; }

        [JsonInclude]
        [JsonPropertyName("minimum-user-level")]
        public MinimumUserLevel MinimumUserLevel { get; private set; }

        [Obsolete("Only for serialization", true)]
        public DynamicCommand() { }

        public DynamicCommand(string commandIdentifier, string commandResponse)
        {
            CommandIdentifier = commandIdentifier;
            CommandResponse = commandResponse;
            MinimumUserLevel = MinimumUserLevel.Viewer;
        }

        public string GetFormattedResponse(string userExecuting, string argument)
        {
            return CommandResponse.Replace(UsernameReplaceLabel, userExecuting).Replace(ArgumentReplaceLabel, argument);
        }
    }
}
