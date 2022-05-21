using OnlineLearning.Common;
using OnlineLearning.Models;

using System.Threading.Tasks;

namespace OnlineLearning.Services
{
    public interface IUserService
    {
        Task<OperationResult<ApplicationUser>> Get(string id);

    }
}