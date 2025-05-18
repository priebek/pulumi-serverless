using App.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace App
{
    public static class NpuCreation
    {
        [FunctionName("CreateNpuItem")]
        public static async Task<IActionResult> CreateItem(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "items")] HttpRequest req)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var item = JsonConvert.DeserializeObject<NpuItem>(requestBody);

            item.Id = Guid.NewGuid().ToString();

            // Send the item to Azure Queue Storage
            var queueClient = CloudStorageAccount.Parse("storageConnectionString").CreateCloudQueueClient();
            var queue = queueClient.GetQueueReference("itemsqueue");
            await queue.CreateIfNotExistsAsync();

            // Create a message to send to the queue
            var message = JsonConvert.SerializeObject(item);
            await queue.AddMessageAsync(new CloudQueueMessage(message));

            return new CreatedResult($"/api/items/{item.Id}", item);
        }

        [FunctionName("GetItem")]
        public static async Task<IActionResult> GetItem(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "items/{id}")] HttpRequest req,
        string id)
        {
            var res = req.HttpContext.Response;
            res.Headers.Append("Access-Control-Allow-Origin", "*");
            res.Headers.Append("Access-Control-Allow-Methods", "GET");
            res.Headers.Append("Content-Type", "application/json");

            var item = new NpuItem { Id = id, Name = "Sample Item", Description = "Sample description" };

            return new OkObjectResult(item);
        }

        [FunctionName("UpdateItem")]
        public static async Task<IActionResult> UpdateItem(
        [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "items/{id}")] HttpRequest req,
        string id)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var updatedItem = JsonConvert.DeserializeObject<NpuItem>(requestBody);
            updatedItem.Id = id;

            return new OkObjectResult(updatedItem);
        }

        [FunctionName("DeleteItem")]
        public static async Task<IActionResult> DeleteItem(
        [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "items/{id}")] HttpRequest req,
        string id)
        {
            return new NoContentResult();
        }
    }
}
