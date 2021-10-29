using OnlineLearning.Common;
using OnlineLearning.Models;

using System;
using System.Threading.Tasks;

namespace OnlineLearning.Services
{
    public interface IIdentityService
    {
        Task<OperationResult<string>> Add(AppDbContext context, string name, string email, string phonenumber, string passwrod, DateTime? birthdate);
        Task<OperationResult<ApplicationUser>> Get(string id);
        Task<OperationResult<string>> Login(string username, string password);
    }
}