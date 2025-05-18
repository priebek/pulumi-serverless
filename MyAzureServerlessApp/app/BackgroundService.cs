using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace App
{
    internal class BackgroundService
    {
        [FunctionName("NewSubmissionHandler")]
        public void HandleNewScore([QueueTrigger("submissions-queue", Connection = "AzureWebJobsStorage")] string msg, ILogger log)
        {
            var doc = JsonDocument.Parse(msg);
            // Add new submission to database
        }

        [FunctionName("NewScoreHandler")]
        public void HandleNewSubmission([QueueTrigger("scores-queue", Connection = "AzureWebJobsStorage")] string msg, ILogger log)
        {
            var doc = JsonDocument.Parse(msg);
            // Update score in database
        }
    }
}
