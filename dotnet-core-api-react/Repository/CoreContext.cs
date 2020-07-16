using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using dotnet_core_api_react.Entities;

namespace dotnet_core_api_react.Repository
{
    public class CoreContext: DbContext
    {
        public DbSet<Customer> Customer { get; set; }
        public CoreContext(DbContextOptions options):base(options)
        {

        }
        
    }
}
