using System;
using System.IO;
using System.Threading.Tasks;
using LittleWins.Data;
using LittleWins.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace LittleWins
{
    public class AutopilotTrigger
    {
        
        private readonly ITableDbContext _tableContext;

        public AutopilotTrigger(ITableDbContext tableDb)
        {
            _tableContext = tableDb ?? throw new ArgumentException();
        }
        
        [FunctionName("AutopilotTrigger")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]
            HttpRequest req, ILogger log)
        {
           
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            log.LogInformation(requestBody);

            var message = new Message(requestBody);

            log.LogInformation(message.Memory);

            var date = DateTime.Parse(message.DateAnswer);

            var littleWinEntity = new LittleWinEntity(message.DetailAnwser, date);

            var result = await _tableContext.InsertOrMergeEntityAsync(littleWinEntity);

            var jsonResponsePositive = @"{ ""actions"":[{""say"": ""You achievement was saved successfully!""}]}";
            var jsonResponseNegative = @"{ ""actions"": [ {""say"": ""There was a problem saving your achievement""}]}";

            return result != null
                ? new ContentResult{Content=jsonResponsePositive, ContentType = "application/json", StatusCode = 200}
                : new ContentResult{Content=jsonResponseNegative, ContentType = "application/json", StatusCode = 418};


        }
    }
}