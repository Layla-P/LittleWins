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
            return null;
        }
    }
}