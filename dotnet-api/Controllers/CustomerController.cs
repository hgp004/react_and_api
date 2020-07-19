using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using dotnet_api.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        [HttpPost]
        public ActionResult CreateUser(UserDto dto)
        {
            dto.Password = "123";
            int.TryParse(dto.UserName.Replace("zcz", ""), out int httpCodeResult);
            //return Ok(dto);
            if (httpCodeResult == 0)
                httpCodeResult = 200;
            return StatusCode(httpCodeResult, dto);
        }
        [HttpGet("{email}")]
        public ActionResult GetUser(string name, int age, [FromRoute]string email, string pwd)
        {
            return Ok(new UserDto
            {
                UserName = name,
                Age = age,
                Email = email,
                Password = pwd
            });
        }
        [HttpPut("{id}")]
        public ActionResult Update(int id, UserDto dto)
        {
            dto.UserName = dto.UserName + id.ToString();
            return Ok(dto);
        }
        [HttpDelete("{id}/{email}")]
        public ActionResult Delete(int id, string email)
        {
            return Ok(new { id, email, success = true});
        }
    }
}
