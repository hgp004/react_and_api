using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_core_api_react.Entities
{
    public class Customer
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
