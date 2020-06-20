using Entities.Models;
using Entities.RequestModels;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.ResponseModels
{
    public class UserResponseModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string token { get; set; }
    }
}
