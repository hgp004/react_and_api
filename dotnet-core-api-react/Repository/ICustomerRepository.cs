using dotnet_core_api_react.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_core_api_react.Repository
{
    public interface ICustomerRepository
    {
        List<Customer> GetCustomers();
    }
}
