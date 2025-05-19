using App.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using System;

using System.Threading.Tasks;

namespace App

{
    public static class Health
    {
        [FunctionName("health")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "health")] HttpRequest req)
        {
            var res = req.HttpContext.Response;
            res.Headers.Append("Access-Control-Allow-Origin", "*");
            res.Headers.Append("Access-Control-Allow-Methods", "GET");

            if (HttpMethods.IsOptions(req.Method))
            {
                return new NoContentResult();
            }

            res.Headers.Append("Content-Type", "application/json");
            var healthResponse = new HealthResponse()
            {
                Status = "healthy",
                Timestamp = DateTime.Now,
            };

            return new OkObjectResult(healthResponse);
        }
    }
}
