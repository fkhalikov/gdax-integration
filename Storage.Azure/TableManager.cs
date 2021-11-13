using Azure.Data.Tables;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Storage.Azure
{
  public class TableManager<T> where T: class, ITableEntity, new()
  {

    
    private TableClient tableClient;

    public TableManager(string tableName)
    {
      tableClient = new TableClient(Environment.GetEnvironmentVariable("AZURE_STORAGE_CONNECTION_STRING"), tableName);
    }

    public void Add(T entity)
    {
      tableClient.AddEntity(entity);
    }

    public IEnumerable<T> GetAll()
    {
      return tableClient.Query<T>(x=>x.PartitionKey == string.Empty).ToList();
    }
  }
}
