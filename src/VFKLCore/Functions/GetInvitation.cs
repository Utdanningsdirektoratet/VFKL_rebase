using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using VFKLCore.Functions.Config;
using VFKLCore.Functions.Models.VFKLInvitation;
using VFKLCore.Functions.Services.Interface;

namespace VFKLCore.Functions
{
    /// <summary>
    /// Api to get assessment informaiton
    /// </summary>
    public class GetInvitation
    {
        private readonly ILogger _logger;
        private readonly VFKLCoreSettings _vfklSettings;
        private readonly IStorage _storage;

        /// <summary>
        /// Get api for assessment
        /// </summary>
        public GetInvitation(ILogger<GetInvitation> logger, IOptions<VFKLCoreSettings> vfklsettings, IStorage storage)
        {
            _logger = logger;
            _vfklSettings = vfklsettings.Value;
            _storage = storage;
        }

        /// <summary>
        /// Get Api for assessment
        /// </summary>
        /// <returns>assessment information for a given assessment id</returns>
        [Function("GetInvitationInformation")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route ="assessment/{id}")] HttpRequestData req, FunctionContext executionContext, string id)
        {
            _logger.LogInformation("Get Assessment request");
            GruppeInvitasjon invitation = await _storage.GetInvitation(id);
            HttpResponseData response = null;

            if (invitation != null)
            {
                response = req.CreateResponse(HttpStatusCode.OK);
                byte[] invitationJson = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(invitation));
                await response.WriteBytesAsync(invitationJson);
            }
            else
            {
                response = req.CreateResponse(HttpStatusCode.NotFound);
                await response.WriteAsJsonAsync("Request failed to fetch invitation information");
            }

            return response;
        }
    }
}
