using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using VFKLCore.Functions.Config;
using VFKLCore.Functions.Extensions;
using VFKLCore.Functions.Services.Interface;

namespace VFKLCore.Functions.Services.Implementation
{
    /// <summary>
    /// Service that downloads data from platform
    /// </summary>
    public class PlatformService : IPlatform
    {
        private readonly HttpClient _client;
        private readonly VFKLCoreSettings _settings;
        private readonly IAuthenticationService _authenticationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlatformService"/> class.
        /// </summary>
        public PlatformService(
            IOptions<VFKLCoreSettings> altinnIntegratorSettings,
            HttpClient httpClient, 
            IAuthenticationService authenticationService)
        {
            _settings = altinnIntegratorSettings.Value;
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));
            _client = httpClient;
            _authenticationService = authenticationService;
        }

        /// <inheritdoc/>
        public async Task<Stream> GetBinaryData(string dataUri)
        {
            string altinnToken = await _authenticationService.GetAltinnToken();

            HttpResponseMessage response = await _client.GetAsync(altinnToken, dataUri);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStreamAsync();
            }
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }

            throw new ApplicationException();
        }
    }
}
