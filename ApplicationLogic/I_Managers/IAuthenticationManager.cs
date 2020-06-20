using Configurations;
using Entities.RequestModels;
using Entities.ResponseModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace ApplicationLogic.I_Managers
{
    public interface IAuthenticationManager
    {
        Task<ResponseData<IdentityResult>> Register(UserProfile user);
        Task<ResponseData<UserResponseModel>> Login(LoginModel user);
    }
}
