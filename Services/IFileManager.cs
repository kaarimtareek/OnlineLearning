using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;

using OnlineLearning.Common;

namespace OnlineLearning.Services
{
    public interface IFileManager
    {
        Task<OperationResult<FileManagerResult>> Add(IFormFile file, string folderName);
        Task<OperationResult<FileManagerResult>> Get(string filePath);
        OperationResult<bool> Delete(string filePath);
    }
}