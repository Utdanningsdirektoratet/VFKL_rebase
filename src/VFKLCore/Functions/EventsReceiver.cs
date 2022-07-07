using System.IO;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using VFKLCore.Functions.Config;
using VFKLCore.Functions.Models;
using VFKLCore.Functions.Services.Interface;

namespace VFKLCore.Functions
{
    /// <summary>
    /// This function is responsible for receving events from Altinn Events.
    /// It will store events in the incomming que for processing by the EventsProcessor
    /// </summary>
    public class EventsReceiver
    {
        private readonly IQueueService _queueService;

        private readonly VFKLCoreSettings _settings;

        private static ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventsReceiver"/> class.
        /// </summary>
        public EventsReceiver(IQueueService queueSerice, ILogger<EventsSubscriber> logger, IOptions<VFKLCoreSettings> altinnIntegratorSettings)
        {
            _settings = altinnIntegratorSettings.Value;
            _queueService = queueSerice;
            _logger = logger;
        }

        /// <summary>
        /// Webhook method to receive CloudEvents from Altinn Platform Events
        /// </summary>
        [Function("EventsReceiver")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequestData req, FunctionContext executionContext)
        {
            CloudEvent cloudEvent = await JsonSerializer.DeserializeAsync<CloudEvent>(req.Body);

            await _queueService.PushToInboundQueue(JsonSerializer.Serialize(cloudEvent));

            string responseMessage = "Cloud Event received and pushed to processing queue";

            HttpResponseData response = req.CreateResponse(HttpStatusCode.OK);
            response.WriteString(responseMessage);

            _logger.LogInformation("Event Id: " + cloudEvent.Id);
            _logger.LogInformation("Event Time: " + cloudEvent.Time);
            _logger.LogInformation("Event type: " + cloudEvent.Type);
            _logger.LogInformation("Event Source: " + cloudEvent.Source);
            _logger.LogInformation("Event Subject: " + cloudEvent.Subject);

            return response;
        }
    }
}