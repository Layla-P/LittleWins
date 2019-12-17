using LittleWins.Models;
using Microsoft.Azure.Cosmos.Table;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LittleWins.Data
{
    public interface ITableDbContext
    {
        Task CreateTableAsync();
        Task<LittleWinEntity> InsertOrMergeEntityAsync(TableEntity entity);
    }
}