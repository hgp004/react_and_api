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
            //ExpressionVisitor
            //Expression.MakeMemberAccess()
            //this.context.Customer.Find
            Type type = typeof(Customer);
            ParameterExpression parameter = Expression.Parameter(type, "a");
            Func<Customer, bool> a = (Customer b) =>
            {
                return b.Name == "a";
            };
            //new Expression<Func<Customer, bool>>();
            //Expression.Lambda<Func<Customer, bool>>()
            Expression<Func<Customer, bool>> filter1 = a => a.Name == "zcz";
            Expression<Func<Customer, bool>> filter2 = b => b.ID == 1;
            var ff = filter1.Joint(filter2);
            //Expression<Func<Customer, bool>>
            //var parameter = filter1.Parameters;
            var body = Expression.And(filter1.Body, filter2.Body);
            var filter = Expression.Lambda<Func<Customer, bool>>(body, parameter);
            return this.query.Where(ff).OrderBy("Name", true).ToList();
        }
    }
}
