using dotnet_core_api_react.Entities;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using dotnet_core_api_react.Extensions;

namespace dotnet_core_api_react.Repository
{
    public class CustomerRepository: ICustomerRepository
    {
        private readonly CoreContext context;
        public IQueryable<Customer> query;
        public CustomerRepository(CoreContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.query = context.Customer.AsQueryable();
        }
        public List<Customer> GetCustomers()
        {
            Expression<Func<Customer, bool>> filter1 = a => a.Name == "zcz";
            Expression<Func<Customer, bool>> filter2 = b => b.ID == 1;
            var ff = filter1.Joint(filter2);
            return this.query.Where(ff).OrderBy("Name", true).ToList();
        }
    }
}
