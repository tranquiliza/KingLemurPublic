using KingLemurJulian.Core.Events;
using MediatR;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace KingLemurJulian.Core.Commands
{
    public class DadJokeCommand : CommandExecutorBase
    {
        private class DadJokeResponse
        {
            [JsonPropertyName("id")]
            public string Id { get; set; }

            [JsonPropertyName("joke")]
            public string Joke { get; set; }

            [JsonPropertyName("status")]
            public int Status { get; set; }
        }

        private const string baseUrlForDadJokes = "https://icanhazdadjoke.com/";

        public override string CommandName => "DadJoke";

        private readonly IMediator mediator;
        private readonly HttpClient httpClient;

        public DadJokeCommand(IMediator mediator, HttpClient httpClient)
        {
            this.mediator = mediator;
            this.httpClient = httpClient;
        }

        public override async Task Execute(CommandRequest commandRequest)
        {
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var json = await httpClient.GetStringAsync(baseUrlForDadJokes).ConfigureAwait(false);
            var result = JsonSerializer.Deserialize<DadJokeResponse>(json);

            await mediator.Send(new CommandResponseRequest(commandRequest, result.Joke)).ConfigureAwait(false);

            httpClient.DefaultRequestHeaders.Clear();
        }
    }
}
