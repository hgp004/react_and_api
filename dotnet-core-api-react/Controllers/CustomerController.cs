using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using dotnet_core_api_react.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_core_api_react.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerControllerUtils utils;
        public CustomerController(CustomerControllerUtils utils)
        {
            this.utils = utils;
        }
        public List<Customer> Get()
        {
            return utils.GetCustomers();
        }       
    }
}
