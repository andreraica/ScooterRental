using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

using Model = ScooterRental.Domain.Models;

namespace AzureFunction.Tracking
{
    public static class TrackingLog
    {
        [FunctionName("TrackingCheck")]
        public static void Run([QueueTrigger("tracking", Connection = "Endpoint=sb://scotterrentalqueue.servicebus.windows.net/;SharedAccessKeyName=SystemUser;SharedAccessKey=;EntityPath=tracking")] Model.Tracking tracking, ILogger log)
        {
            //new TrackingService(new  ().Insert(tracking.ScooterId, tracking.LocationId);
        }
    }
}
