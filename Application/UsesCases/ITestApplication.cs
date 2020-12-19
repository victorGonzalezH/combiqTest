using Combiq.Test.Application.UsesCases.NewCustomer;

namespace Combiq.Test
{

    /// <summary>
    /// Interface de aplicacion
    /// </summary>
    public interface ITestApplication
    {
        /// <summary>
        /// Crea un nuevo cliente
        /// </summary>
        /// <param name="newCustomerCommand"></param>
        /// <returns></returns>
        NewCustomerResultDto CreateCustomer(NewCustomerCommand newCustomerCommand);
        
        
        /// <summary>
        /// Guarda un nuevo cliente
        /// </summary>
        /// <param name="newCustomerCommandString"></param>
        void SaveCustomer(string newCustomerCommandString);

    }

}