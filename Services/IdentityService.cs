using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OnlineLearning.Common;
using OnlineLearning.Constants;
using OnlineLearning.Models;
using OnlineLearning.Settings;

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearning.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly DbContextOptions<AppDbContext> contextOptions;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly JwtSettings jwtSettings;
        private readonly int validTokenDaysNumber = 7;
        private readonly RoleManager<IdentityRole> roleManager;

        public IdentityService(DbContextOptions<AppDbContext> contextOptions, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,RoleManager<IdentityRole> roleManager, JwtSettings jwtSettings)
        {
            this.contextOptions = contextOptions;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.jwtSettings = jwtSettings;
            this.roleManager = roleManager;
        }

        public async Task<OperationResult<string>> Add(AppDbContext context, string name, string email, string phonenumber, string passwrod, DateTime? birthdate)
        {
            try
            {

                email = email.Trim();
                name = name.Trim();
                phonenumber = phonenumber.Trim();
                var user = new ApplicationUser
                {
                    Email = email,
                    PhoneNumber = phonenumber,
                    Name = name,
                    Birthdate = birthdate,
                    UserName = email,
                };
                //create the user
               var createUserResult = await userManager.CreateAsync(user, passwrod);
                if(!createUserResult.Succeeded)
                {
                    return OperationResult.Fail<string>(ConstantMessageCodes.OPERATION_FAILED, default, ResponseCodeEnum.FAILED);

                }
                await context.SaveChangesAsync();
                var customerRole = await roleManager.FindByNameAsync(ConstantUserRoles.Customer);
                var createdUser = await  userManager.FindByNameAsync(user.UserName);
                if(customerRole ==null)
                {
                    var createRoleResult  = await roleManager.CreateAsync(new IdentityRole(ConstantUserRoles.Customer));
                    if(!createRoleResult.Succeeded)
                    return OperationResult.Fail<string>(ConstantMessageCodes.OPERATION_FAILED, default, ResponseCodeEnum.FAILED);

                }
                customerRole = await roleManager.FindByNameAsync(ConstantUserRoles.Customer);
                //create the claims and roles for the user
                var roleResult = await userManager.AddToRoleAsync(createdUser, customerRole.Name);
                if(!roleResult.Succeeded)
                {
                    return OperationResult.Fail<string>(ConstantMessageCodes.OPERATION_FAILED, default, ResponseCodeEnum.FAILED);

                }
                var token = CreateToken(user.Email, user.Id, validTokenDaysNumber);
                return OperationResult.Success(ConstantMessageCodes.OPERATION_SUCCESS, token, ResponseCodeEnum.SUCCESS);

            }
            catch (Exception e)
            {
                return OperationResult.Fail<string>(ConstantMessageCodes.OPERATION_FAILED, default, ResponseCodeEnum.FAILED);
            }
        }

        public async Task<OperationResult<ApplicationUser>> Get(string id)
        {
            try
            {
                using (var context = new AppDbContext(contextOptions))
                {
                    var user = await context.ApplicationUsers.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
                    if (user == null)
                    {
                        return OperationResult.Fail<ApplicationUser>(ConstantMessageCodes.USER_NOT_FOUND, default, ResponseCodeEnum.FAILED);
                    }
                    return OperationResult.Success(ConstantMessageCodes.OPERATION_SUCCESS, user, ResponseCodeEnum.SUCCESS);
                }
            }
            catch (Exception e)
            {
                return OperationResult.Fail<ApplicationUser>(ConstantMessageCodes.OPERATION_FAILED, default, ResponseCodeEnum.FAILED);
            }
        }
        public async Task<OperationResult<string>> Login(string username, string password)
        {
            try
            {

                var user = await userManager.FindByEmailAsync(username);
                if (user == null)
                {
                    return OperationResult.Fail<string>(ConstantMessageCodes.OPERATION_FAILED, default, ResponseCodeEnum.FAILED);
                }
                var signInResult = await signInManager.PasswordSignInAsync(user, password, true, false);
                if (!signInResult.Succeeded)
                {
                    return OperationResult.Fail<string>(ConstantMessageCodes.OPERATION_FAILED, default, ResponseCodeEnum.FAILED);
                }
                var token = CreateToken(user.Email, user.Id, validTokenDaysNumber);
                return OperationResult.Success(ConstantMessageCodes.OPERATION_SUCCESS, token, ResponseCodeEnum.SUCCESS);

            }
            catch (Exception e)
            {
                return OperationResult.Fail<string>(ConstantMessageCodes.OPERATION_FAILED, default, ResponseCodeEnum.FAILED);
            }
        }
        private string CreateToken(string email, string id, int validTokenDays)
        {
            var tokenHandler = new JwtSecurityTokenHandler
            {

            };
            var key = Encoding.ASCII.GetBytes(jwtSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]{
                        new Claim(JwtRegisteredClaimNames.Sub, email),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Email, email),
                        new Claim(ConstantSecurityHeaders.Id, id),
                        }),
                Expires = DateTime.UtcNow.AddDays(validTokenDays),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
