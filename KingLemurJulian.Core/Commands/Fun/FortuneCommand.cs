using KingLemurJulian.Core.Events;
using MediatR;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace KingLemurJulian.Core.Commands
{
    public class FortuneCommand : CommandExecutorBase
    {
        private class FortuneResponse
        {
            [JsonPropertyName("fortune")]
            public string Fortune { get; set; }
        }

        private const string fortuneApiUrl = "http://yerkee.com/api/fortune";

        public override string CommandName => "Fortune";

        private readonly IMediator mediator;
        private readonly HttpClient httpClient;

        public FortuneCommand(IMediator mediator, HttpClient httpClient)
        {
            this.mediator = mediator;
            this.httpClient = httpClient;
        }

        public override async Task Execute(CommandRequest commandRequest)
        {
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var json = await httpClient.GetStringAsync(fortuneApiUrl).ConfigureAwait(false);
            var result = JsonSerializer.Deserialize<FortuneResponse>(json);

            await mediator.Send(new CommandResponseRequest(commandRequest, result.Fortune)).ConfigureAwait(false);

            httpClient.DefaultRequestHeaders.Clear();
        }
    }
}
