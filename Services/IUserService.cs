using OnlineLearning.Common;
using OnlineLearning.Models;

using System;
using System.Threading.Tasks;

namespace OnlineLearning.Services
{
    public interface IUserService
    {
        Task<OperationResult<int>> Add(string name, string email, string phonenumber, string passwrod, DateTime? birthdate);
        Task<OperationResult<User>> Get(int id);
    }
}