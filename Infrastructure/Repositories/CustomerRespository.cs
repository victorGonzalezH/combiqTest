
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Extensions.Logging;
using System;

namespace Combiq.Test.Infrastructure.Repositories
{

    public class CustomerRepository: ICustomerRepository
    {
        private string connectionString;
        private CloudTable customerTable;

        private ILogger log;

        public CustomerRepository(string connectionString, ILogger log)
        {
            // Asigna la cadena de conexion
            this.connectionString   = connectionString;

            this.log = log;

            // Crea la table de clientes
            this.customerTable      = this.CreateTable("customers", true);
        }


         private CloudTable CreateTable(string tableName, bool createIfNotExists = false)
         {
            CloudStorageAccount account = CloudStorageAccount.Parse(this.connectionString);
            CloudTableClient client = account.CreateCloudTableClient();

            var table = client.GetTableReference(tableName);

            if (createIfNotExists)
            {
                table.CreateIfNotExists();
            }

            return table;
         }
    
    
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="table"></param>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        public void AddCustomer<T>(T value) where T : ITableEntity
        {
            if (this.customerTable == null)
            {
                throw new ArgumentNullException(nameof(customerTable));
            }

            TableOperation operation = TableOperation.InsertOrReplace(value);
            this.customerTable.Execute(operation);
            this.log.LogInformation("Cliente guardado en el storage");
        }
    }

}
