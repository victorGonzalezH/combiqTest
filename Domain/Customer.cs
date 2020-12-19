using Microsoft.Azure.Cosmos.Table;

namespace Combiq.Test.Domain
{

    public class Customer: TableEntity
    {
        public string Name {get; set;}

        public string Address {get; set;}

        public string Phone {get; set;}

        public string Email {get; set;}

        public string Age {get; set;}
    }

}