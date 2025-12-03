using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserIdentityProject.Helpers;
using UserIdentityProject.Models;

namespace UserIdentityProject.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JwtOptions _jwt;
        public AuthService(UserManager<ApplicationUser> userManager, IOptions<JwtOptions> jwt, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _jwt = jwt.Value;
            _roleManager = roleManager;
        }

        public async Task<AuthModel> RegisterAsync(RegisterModel model)
        {
            if (await _userManager.FindByEmailAsync(model.Email) is not null)
                return new AuthModel { Message = "Email is already registered!" };

            if (await _userManager.FindByNameAsync(model.Username) is not null)
                return new AuthModel { Message = "Username is already registered!" };

            var user = new ApplicationUser
            {
                UserName = model.Username,
                Email = model.Email,
                FristName = model.FristName,
                LastName = model.LastName
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                var errors = string.Empty;

                foreach (var error in result.Errors)
                    errors += $"{error.Description},";

                return new AuthModel { Message = errors };
            }

            await _userManager.AddToRoleAsync(user, "User");

            var jwtSecurityToken = await CreateJwtToken(user);

            return new AuthModel
            {
                Email = user.Email,
                ExpireOn = jwtSecurityToken.ValidTo,
                IsAuthenticated = true,
                Roles = new List<string> { "User" },
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                UserName = user.UserName
            };
        }




        public async Task<AuthModel> GetTokenAsync(TokenRequestModel model)
        {
            var authModel = new AuthModel();

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user is null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                authModel.Message = "Email or Password is incorrect!";
                return authModel;
            }

            var jwtSecurityToken = await CreateJwtToken(user);
            var rolesList = await _userManager.GetRolesAsync(user);

            authModel.IsAuthenticated = true;
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            authModel.Email = user.Email;
            authModel.UserName = user.UserName;
            authModel.ExpireOn = jwtSecurityToken.ValidTo;
            authModel.Roles = rolesList.ToList();

            return authModel;
        }



        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
           {
               var userClaim = await _userManager.GetClaimsAsync(user);
               var roles = await _userManager.GetRolesAsync(user);
               var roleClaims = new List<Claim>();
               foreach (var role in roles)
               {
                   roleClaims.Add(new Claim("roles", role));
               }
               var claims = new[]
               {
                   new Claim (JwtRegisteredClaimNames.Sub,user.UserName),
                   new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                   new Claim(JwtRegisteredClaimNames.Email,user.Email),
                   new Claim("uid",user.Id)
               }.Union(userClaim).Union(roleClaims);

               var SymmatricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
               var signingCredintal = new SigningCredentials(SymmatricSecurityKey, SecurityAlgorithms.HmacSha256);
               var jwtSecurityToken = new JwtSecurityToken
                   (
                   issuer : _jwt.Issuer,
                   audience : _jwt.Audience,
                   claims:claims,
                   expires:DateTime.Now.AddDays(_jwt.Lifetime),
                   signingCredentials : signingCredintal);
               return jwtSecurityToken;
           }

        public async Task<string> AddRoleAsync(AddRoleModel model)
        {
            var user = await _userManager.FindByIdAsync(model.userId);
            if (user is null || !await _roleManager.RoleExistsAsync(model.Role))
                return "User Id Or Role Is Invalid";
            if (await _userManager.IsInRoleAsync(user, model.Role))
                return "User already assigned to this role";
            var result = await _userManager.AddToRoleAsync(user, model.Role);

            return result.Succeeded ? string.Empty : "Something went Wrong";
        }
    }
}
