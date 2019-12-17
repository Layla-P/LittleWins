using Microsoft.Azure.Cosmos.Table;
using System;

namespace LittleWins.Models
{
    public class LittleWinEntity : TableEntity
    {

        public LittleWinEntity()
        {}

        public LittleWinEntity(string details, DateTime date)
        {
            PartitionKey = "Wins";
            RowKey = Guid.NewGuid().ToString();
            Details = details;
            DateAchieved = date;
        }

        public string Details { get; set; }
        public DateTime DateAchieved { get; set; }
    }
}
