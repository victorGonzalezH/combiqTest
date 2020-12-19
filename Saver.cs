using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Combiq.Test.Application.UsesCases.NewCustomer;

namespace Combiq.Test
{
    public static class Saver
    {
        [FunctionName("Saver")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req, ILogger log)
        {
            log.LogInformation("Processing request");

            // Se obtiene el cuerpo de la solicitud y se convierte en cadena. Esta cadena contendra el comando
            // para crear un nuevo client
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            // Se deserializa la cadena para obtener el comando
            NewCustomerCommand newCustomerCommand = JsonConvert.DeserializeObject<NewCustomerCommand>(requestBody);
            
            // Por simplcidad el objeto applicacion, se instancia aqui, pero se puede inyectar por medio de DI
            ITestApplication testApplication = new TestApplication(log);

            // Se agrega el nuevo cliente
            var createCustomerResult = testApplication.CreateCustomer(newCustomerCommand);

            return new OkObjectResult(createCustomerResult);
        }
    }
}
