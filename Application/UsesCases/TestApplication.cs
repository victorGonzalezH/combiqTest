using System;
using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

using Combiq.Test.Application.UsesCases.NewCustomer;
using Combiq.Test.Domain;
using Combiq.Test.Infrastructure.Repositories;


namespace Combiq.Test
{
    public class TestApplication: ITestApplication
    {
        public static int counter = 0;

        /// <summary>
        /// Cadena de conexion
        /// </summary>
        private string connectionString;

        /// <summary>
        /// Cliente para la conexion hacia la cola de mensajes de nuevos clientes
        /// </summary>
        private QueueClient queueClient;

        // Logger
        private ILogger log;


        private ICustomerRepository customerRepository;

        /// <summary>
        /// Constructor.
        /// </summary>
        public TestApplication(ILogger log) // 
        {
            
            this.log = log;

            //Por simplicidad el valor de la cadena de conexion hacia la cola se declara aqui, pero 
            // se puede guardar en un archivo de configuracion y mediante DI obtenerla
            connectionString = "DefaultEndpointsProtocol=http;AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;QueueEndpoint=http://127.0.0.1:10001/devstoreaccount1";
            string tableStorageConnectionString = "AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;DefaultEndpointsProtocol=http;BlobEndpoint=http://127.0.0.1:10000/devstoreaccount1;QueueEndpoint=http://127.0.0.1:10001/devstoreaccount1;TableEndpoint=http://127.0.0.1:10002/devstoreaccount1;";
            
            // POR SIMPLICIDAD SE INSTANCIA EL REPOSITORI DE CLIENTES AQUI, PERO ES MEJOR INYECTARLO MEDIANTE DI
            this.customerRepository = new CustomerRepository(tableStorageConnectionString, this.log);
            try
            {
                // Instanciacion del cliente
                this.queueClient = this.CreateQueue("customersqueue", connectionString, this.log);
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }


    /// <summary>
    /// Crea un nuevo cliente/creates a new customer
    /// </summary>
    /// <param name="newCustomerCommand"></param>
    /// <returns></returns>
    public NewCustomerResultDto CreateCustomer(NewCustomerCommand newCustomerCommand)
        {
            try
            {
                if (queueClient.Exists())
                {
                    // Send a message to the queue
                    string serializedCommand = JsonConvert.SerializeObject(newCustomerCommand);
                    Console.WriteLine(serializedCommand);
                    var result = this.queueClient.SendMessage(this.Base64Encode(serializedCommand));
                    this.log.LogInformation($"Nuevo cliente en la cola: '{result.Value.MessageId}'");
                    return new NewCustomerResultDto(){ Id = result.Value.MessageId };

                }
            
                return new NewCustomerResultDto(){ Id = null};;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }

    public void SaveCustomer(string newCustomerCommandString)
    {
        NewCustomerCommand newCustomerCommand = JsonConvert.DeserializeObject<NewCustomerCommand>(newCustomerCommandString);
        this.SaveCustomer(newCustomerCommand);
    }


    /// <summary>
    /// Guarda un objeto customer en el storage
    /// </summary>
    /// <param name="newCustomerCommand"></param>
    public void SaveCustomer(NewCustomerCommand newCustomerCommand)
    {
        try
        {
            counter++;
                // Se crea el objeto de domini customer
            Customer customer = new Customer() {

                Address = newCustomerCommand.Address,
                Age = newCustomerCommand.Age,
                Email = newCustomerCommand.Email,
                Name = newCustomerCommand.Name,
                Phone = newCustomerCommand.Phone,
                PartitionKey = newCustomerCommand.Name,
                RowKey = counter.ToString()

            };

            this.customerRepository.AddCustomer<Customer>(customer);
        }
        catch(Exception ex)
        {
            throw ex;
        }

        
    }



    /// <summary>
    /// Crea el cliente para acceder a la cola de los nuevos clientes
    /// </summary>
    /// <param name="queueName">Nombre de la cola</param>
    /// <param name="connectionString">Cadena de conexion</param>
    /// <param name="log">Logger</param>
    /// <returns></returns>
    private QueueClient CreateQueue(string queueName, string connectionString, ILogger log)
    {
        try
        {
            
            //Instancia cliente para la manipulacion de la cola
            QueueClient queueClient = new QueueClient(connectionString, queueName);

            // Crea la cola si no existe
            queueClient.CreateIfNotExists();

            if (queueClient.Exists())
            {
                log.LogInformation($"Cola de clientes ya esta creada: '{queueClient.Name}'");
                return queueClient;
            }
            else
            {
                log.LogInformation($"Asegurate que el servicio de almacenamiento local se esta ejecutando, por ejempl azurite");
                return null;
            }
        }
        catch (Exception ex)
        {
            log.LogInformation($"Exception: {ex.Message}\n\n");
            log.LogInformation($"Asegurate que el servicio de almacenamiento local se esta ejecutando, por ejempl azurite");
            throw ex;
        }
    }

        
    /// <summary>
    /// Convierte una cadena a formato 64
    /// </summary>
    /// <param name="plainText"></param>
    /// <returns></returns>
    private string Base64Encode(string plainText)
    {
        var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
        return System.Convert.ToBase64String(plainTextBytes);
    }


    }

}