using KingLemurJulian.Core.Events;
using MediatR;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace KingLemurJulian.Core.Commands
{
    public class JokeCommand : CommandExecutorBase
    {
        private class FunResponse
        {
            [JsonPropertyName("error")]
            public bool Error { get; set; }

            [JsonPropertyName("category")]
            public string Category { get; set; }

            [JsonPropertyName("type")]
            public string Type { get; set; }

            [JsonPropertyName("setup")]
            public string Setup { get; set; }

            [JsonPropertyName("delivery")]
            public string Delivery { get; set; }

            [JsonPropertyName("joke")]
            public string Joke { get; set; }

            [JsonPropertyName("flags")]
            public Flags Flags { get; set; }

            [JsonPropertyName("id")]
            public int Id { get; set; }

            [JsonPropertyName("safe")]
            public bool Safe { get; set; }

            [JsonPropertyName("lang")]
            public string Language { get; set; }

            public bool IsTwoPart => string.Equals("twopart", Type, StringComparison.OrdinalIgnoreCase);
        }

        private class Flags
        {
            [JsonPropertyName("nsfw")]
            public bool Nsfw { get; set; }

            [JsonPropertyName("religious")]
            public bool Religious { get; set; }

            [JsonPropertyName("political")]
            public bool Political { get; set; }

            [JsonPropertyName("racist")]
            public bool Racist { get; set; }

            [JsonPropertyName("sexist")]
            public bool Sexist { get; set; }

            [JsonPropertyName("explicit")]
            public bool Explicit { get; set; }
        }

        private const string baseUrlForFuns = "https://v2.jokeapi.dev/joke/Programming,Miscellaneous,Pun,Spooky?blacklistFlags=nsfw,religious,political,racist,sexist";
        public override string CommandName => "Joke";

        private readonly IMediator mediator;
        private readonly HttpClient httpClient;

        public JokeCommand(IMediator mediator, HttpClient httpClient)
        {
            this.mediator = mediator;
            this.httpClient = httpClient;
        }

        public override async Task Execute(CommandRequest commandRequest)
        {
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var validJoke = await GetValidJoke().ConfigureAwait(false);
            if (validJoke.IsTwoPart)
            {
                await mediator.Send(new CommandResponseRequest(commandRequest, validJoke.Setup)).ConfigureAwait(false);

                await Task.Delay(TimeSpan.FromSeconds(5)).ConfigureAwait(false);

                await mediator.Send(new CommandResponseRequest(commandRequest, validJoke.Delivery)).ConfigureAwait(false);
            }
            else
            {
                await mediator.Send(new CommandResponseRequest(commandRequest, validJoke.Joke)).ConfigureAwait(false);
            }

            httpClient.DefaultRequestHeaders.Clear();
        }

        private async Task<FunResponse> GetValidJoke()
        {
            var json = await httpClient.GetStringAsync(baseUrlForFuns).ConfigureAwait(false);
            var requestResult = JsonSerializer.Deserialize<FunResponse>(json);

            if (!requestResult.Safe)
                return await GetValidJoke().ConfigureAwait(false);
            else
                return requestResult;
        }
    }
}
