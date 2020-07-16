using dotnet_core_api_react.Entities;
using dotnet_core_api_react.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace dotnet_core_api_react.Controllers
{
    public class CustomerControllerUtils
    {
        private readonly ICustomerRepository customerRepository;
        public CustomerControllerUtils(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }
        public List<Customer> GetCustomers()
        {
            Func<string, string> del = a => a + "123";
            Func<string, string> del2 = (string a) => { return a + "123"; };

            //typeof(((string a) => { return a + "123"; } ))
            //Expression< Func<string, string>> expression = (LambdaExpression)del;

            return this.customerRepository.GetCustomers();
        }
    }
}
