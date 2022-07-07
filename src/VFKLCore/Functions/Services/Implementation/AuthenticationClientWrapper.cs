using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using VFKLCore.Functions.Config;
using VFKLCore.Functions.Extensions;
using VFKLCore.Functions.Services.Interface;

namespace VFKLCore.Functions.Services.Implementation
{
    /// <summary>
    /// HttpClient wrapper responsible for calling Altinn Platform Authentication to convert MaskinPorten token to AltinnToken
    /// </summary>
    public class AuthenticationClientWrapper : IAuthenticationClientWrapper
    {
        private readonly HttpClient _client;

        private readonly VFKLCoreSettings _settings;

        /// <summary>
        /// Gets or sets the base address
        /// </summary>
        private string BaseAddress { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationClientWrapper" /> class.
        /// </summary>
        public AuthenticationClientWrapper(IOptions<VFKLCoreSettings> altinnIntegratorSettings, HttpClient httpClient)
        {
            _settings = altinnIntegratorSettings.Value;
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));
            _client = httpClient;
        }

        /// <inheritdoc/>
        public async Task<string> ConvertToken(string token)
        {
            string cmd = $@"{_settings.PlatformBaseUrl}authentication/api/v1/exchange/maskinporten?test={_settings.TestMode}";
            HttpResponseMessage response = await _client.GetAsync(token, cmd);

            if (response.IsSuccessStatusCode)
            {
                string jsonString = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<string>(jsonString);
            }
            else
            {
                return $@"Could not retrieve Altinn Token";
            }
        }
    }
}
