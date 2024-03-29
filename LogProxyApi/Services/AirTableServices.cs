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
        public AirTableService(){}

        public AirTableService(HttpClient client, IOptions<ApiSettings> apiSettings, ILogger<AirTableService> logger)
        {
            client.BaseAddress = new Uri($"{apiSettings.Value.Host}");
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"{apiSettings.Value.ApiKey}");
            Client = client;
            this._logger = logger;
            this.apiSettings = apiSettings.Value;
        }

        public virtual async Task<AirTableLogResponse> GetAllMessages()
        {
            return await Client.GetFromJsonAsync<AirTableLogResponse>($"/{apiSettings.Prefix}/Messages");
        }

        public virtual async Task<LogRecordResponse> CreateMessage(LogRequest request)
        {
            var options = new JsonSerializerOptions{ PropertyNamingPolicy = null };
            using var httpResponse = await Client.PostAsJsonAsync($"{apiSettings.Prefix}/Messages", request, options);
            
            httpResponse.EnsureSuccessStatusCode();
            return await httpResponse.Content.ReadFromJsonAsync<LogRecordResponse>();
        }
    }
}