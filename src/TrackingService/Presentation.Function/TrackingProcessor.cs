using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace Presentation.Function
{
    public static class TrackingProcessor
    {
        [FunctionName("TrackingProcessor")]
        public static void Run([QueueTrigger("tracking", Connection = "TRACKING_QUEUE_CONNECTION")]string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
        }
    }
}
