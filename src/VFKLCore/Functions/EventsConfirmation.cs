using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using VFKLCore.Functions.Models;
using VFKLCore.Functions.Services.Interface;

namespace VFKLCore.Functions
{
    /// <summary>
    /// Azure Function that confirmes that data for a given instance is downloaded
    /// </summary>
    public class EventsConfirmation
    {
        private readonly IAltinnApp _altinnApp;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventsConfirmation"/> class.
        /// </summary>
        public EventsConfirmation(IAltinnApp altinnApp)
        {
            _altinnApp = altinnApp;
        }

        /// <summary>
        /// Function method that is triggered by new element on events-confirmation queue
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Function("EventsConfirmation")]
        public async Task Run([QueueTrigger("events-confirmation", Connection = "QueueStorageSettings:ConnectionString")]string item, FunctionContext executionContext)
        {
            CloudEvent cloudEvent = JsonSerializer.Deserialize<CloudEvent>(item);

            await _altinnApp.AddCompleteConfirmation(cloudEvent.Source.AbsoluteUri);
        }
    }
}
