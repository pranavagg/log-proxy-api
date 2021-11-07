using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using LogProxyApi.Models;
using Microsoft.Extensions.Logging;
using LogProxyApi.Helpers;
using Microsoft.Extensions.Options;

namespace LogProxyApi.Services
{
    public class AirTableService
    {
        public HttpClient Client { get; }

        private readonly ILogger<AirTableService> _logger;
        
        private readonly ApiSettings apiSettings;

        public AirTableService(HttpClient client, IOptions<ApiSettings> apiSettings, ILogger<AirTableService> logger)
        {
            client.BaseAddress = new Uri($"{apiSettings.Value.Host}");
            // GitHub API versioning
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            // GitHub requires a user-agent
            client.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactory-Sample");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"{apiSettings.Value.ApiKey}");
            Client = client;
            this._logger = logger;
            this.apiSettings = apiSettings.Value;
        }

        public async Task<AirTableLogResponse> GetAllMessages()
        {
            return await Client.GetFromJsonAsync<AirTableLogResponse>($"/{apiSettings.Prefix}/Messages");
        }

        public async Task CreateMessage(LogRequest request)
        {
            // var json = JsonSerializer.Serialize(request);
            // this._logger.LogInformation(json);
            // using var httpResponse = await Client.PostAsync("/v0/appD1b1YjWoXkUJwR/Messages", new StringContent(json));

            var options = new JsonSerializerOptions{ PropertyNamingPolicy = null };
            using var httpResponse = await Client.PostAsJsonAsync($"{apiSettings.Prefix}/Messages", request, options);
            this._logger.LogInformation(await httpResponse.Content.ReadAsStringAsync());
            httpResponse.EnsureSuccessStatusCode();
        }
    }
}