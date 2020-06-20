using ApplicationLogic.I_Managers;
using Configurations;
using Entities.RequestModels;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Entities.ResponseModels;
using Microsoft.Extensions.Options;
using Configurations.Helpers;

namespace ApplicationLogic.Managers
{
    public class AuthenticationManager : IAuthenticationManager
    {
        private readonly AuthenticationContext _authenticationContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationSetting appSettings;

        public AuthenticationManager(AuthenticationContext authenticationContext, SignInManager<ApplicationUser> _signInManager, UserManager<ApplicationUser> userManager, IOptions<ApplicationSetting> appSettings)
        {
            this._authenticationContext = authenticationContext;
            this._userManager = userManager;
            this.appSettings = appSettings.Value;
            this._signInManager = _signInManager;
        }
        public async Task<ResponseData<UserResponseModel>> Login(LoginModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, isPersistent: false, lockoutOnFailure: true);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(model.Username);
                var descriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[] { new Claim("UserID", user.Id.ToString()) }),
                    Expires = DateTime.UtcNow.AddDays(10),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var Securitytoken = tokenHandler.CreateToken(descriptor);
                var token = tokenHandler.WriteToken(Securitytoken);

                return new ResponseData<UserResponseModel>()
                {
                    Message = Messages.UserLoggedInSuccessfully,
                    Data = new UserResponseModel() { Name = user.Name, Email = user.Email, token = token },
                    Success = true,
                    Count = 1
                };
            }
            return new ResponseData<UserResponseModel>()
            {
                Message = Messages.UserCredentialsInvalid,
                Data = null,
                Success = false
            };
        }

        public async Task<ResponseData<IdentityResult>> Register(UserProfile user)
        {
            var applicationUser = new ApplicationUser()
            {
                UserName = user.Username,
                Email = user.Email,
                Name = user.Name
            };
            var result = await this._userManager.CreateAsync(applicationUser, user.Password);
            return new ResponseData<IdentityResult>()
            {
                Data = result,
                Success = result.Succeeded,
                Message = result.Succeeded ? Messages.UserRegisteredSuccessfully : Messages.ErrorOccurred,
                Count = 1
            };
        }
    }
}
