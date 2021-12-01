using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using s3loginbackend.Logic;
using s3loginbackend.Models;
using Microsoft.AspNetCore.Http;

namespace s3loginbackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : Controller
    {
        [HttpPost("CreateUser")]
        public int CreateUser(string username, string password)
        {
            LoginLogic loginLogic = new LoginLogic();
            if (loginLogic.CreateUser(username, password))
            {
                return StatusCodes.Status200OK;
            }
            else
            {
                return StatusCodes.Status500InternalServerError;
            }
        }

        [HttpGet("Login")]
        public UserModel Login(string username, string password)
        {
            LoginLogic loginLogic = new LoginLogic();
            UserModel userDetails = loginLogic.Login(username, password);

            return userDetails;
        }

        [HttpPatch("UpdateUser")]
        public int UpdateUser(string username, string password, string newUsername, int userId)
        {
            LoginLogic loginLogic = new LoginLogic();
            if (loginLogic.UpdateUser(username, password, newUsername, userId) == 1)
            {
                return StatusCodes.Status200OK;
            }
            else
            {
                return StatusCodes.Status500InternalServerError;
            }
        }

        [HttpDelete("DeleteUser")]
        public int DeleteUser(string username, string password, int userId)
        {
            LoginLogic loginLogic = new LoginLogic();
            if (loginLogic.DeleteUser(username, password, userId) == 1)
            {
                return StatusCodes.Status200OK;
            }
            else
            {
                return StatusCodes.Status500InternalServerError;
            }
        }
    }
}
