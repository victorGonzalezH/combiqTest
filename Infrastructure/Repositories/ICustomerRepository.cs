
using Microsoft.Azure.Cosmos.Table;
using System;

namespace Combiq.Test.Infrastructure.Repositories
{

    public interface ICustomerRepository
    {
        /// <summary>
        /// Agrega un nuevo cliente y lo guarda en el storage
        /// </summary>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        void AddCustomer<T>(T value) where T : ITableEntity;

    }
    
}