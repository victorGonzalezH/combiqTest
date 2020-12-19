using System;

namespace Combiq.Test.Application.UsesCases.NewCustomer
{

    public class NewCustomerCommand
    {
        /// <summary>
        /// Name of the new customer
        /// </summary>
        /// <value></value>
        public string Name {get; set;}


        /// <summary>
        /// Address of the customer
        /// </summary>
        /// <value></value>
        public string Address {get; set;}

        /// <summary>
        /// Phone od the customer
        /// </summary>
        /// <value></value>
        public string Phone {get; set;}

        
        /// <summary>
        /// Email of the customer
        /// </summary>
        /// <value></value>
        public string Email {get; set;}


        /// <summary>
        /// Age of the customer
        /// </summary>
        /// <value></value>
        public string Age {get; set;}


        public NewCustomerCommand()
        {

        }
    }

}