using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationLogic.I_Managers;
using Entities.Models;
using Entities.RequestModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MaddrassAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationManager _authenticationManager;
        public AuthenticationController(IAuthenticationManager authenticationManager)
        {
            this._authenticationManager = authenticationManager;
        }

        [HttpPost,Route("register")]
        public async Task<IActionResult> Register(UserProfile user)
        {
            var result = await this._authenticationManager.Register(user);
            return Ok(result);
        }
        
        [HttpPost, Route("login")]
        public async Task<IActionResult> Login(LoginModel user)
        {
            var result = await this._authenticationManager.Login(user);
            return Ok(result);
        }
    }
}