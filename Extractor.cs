using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using Newtonsoft.Json;

namespace Combiq.Test
{
    public static class Extractor
    {
        [FunctionName("Extractor")]
        public static void Run(
            [QueueTrigger("customersqueue", Connection = "QueueCustomerConnectionString" ) ] string newCustomerCommandString, 
            ILogger log)
        {
            log.LogInformation($"Nuevo comando para crear el cliente: {newCustomerCommandString}");

            // POR SIMPLICIDAD SE INSTANCIA EL OBJETO APLICACION AQUI, AUNQUE ES MEJOR HACERLO POR DI
            ITestApplication testApplication = new TestApplication(log);
            
            // Se guarda el nuevo cliente
            testApplication.SaveCustomer(newCustomerCommandString);
            
        }
    }

}

