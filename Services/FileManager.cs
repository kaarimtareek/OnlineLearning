using System;
using System.IO;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;

using OnlineLearning.Common;
using OnlineLearning.Constants;
using OnlineLearning.Utilities;

namespace OnlineLearning.Services
{
    public class FileManager : IFileManager
    {
        private readonly ILoggerService<FileManager> logger;

        public FileManager(ILoggerService<FileManager> logger)
        {
            this.logger = logger;
        }

        public async Task<OperationResult<FileManagerResult>> Add(IFormFile file, string folderName,string parentFolder ="")
        {
            string filename = file.FileName;
            try
            {
                string contentType;
                var provider = new FileExtensionContentTypeProvider();
                provider.TryGetContentType(filename, out contentType);
                string currentDirectory = Directory.GetCurrentDirectory();
                string folderPath;
                if(string.IsNullOrEmpty(parentFolder))
                     folderPath = Path.Combine(currentDirectory, folderName);
                else
                    folderPath = Path.Combine(currentDirectory,parentFolder, folderName);

                //if the directory is not there , it will create it , otherwise it will ignore
                Directory.CreateDirectory(folderPath);
                string fullPath = Path.Combine(folderPath, file.FileName);
                //if the file is there , it will overwrite it
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                return OperationResult.Success( new FileManagerResult
                {
                    ContentType = contentType,
                    FileName = filename,
                    FullPath = fullPath
                });
            }
            catch (Exception e)
            {
                logger.LogError($"error while adding file {filename} ,error {e}");
                return OperationResult.Fail<FileManagerResult>();
            }
        }

        public async Task<OperationResult<FileManagerResult>> Get(string filePath)
        {
            var result = new OperationResult<FileManagerResult>
            {
                IsSuccess = true,
                Message = ConstantMessageCodes.OPERATION_SUCCESS,
                ResponseCode = ResponseCodeEnum.SUCCESS,
            };
            try
            {
                var file = File.ReadAllBytes(filePath);
                var provider = new FileExtensionContentTypeProvider();
                string filename = Path.GetFileName(filePath);
                string contentType;
                provider.TryGetContentType(filename, out contentType);
                result.Data = new FileManagerResult
                {
                    Content = file,
                    FileName = filename,
                    ContentType = contentType
                };
                return result;
            }
            catch (FileNotFoundException e)
            {
                logger.LogError($"error while getting file with path {filePath} ,error {e}");

                result.IsSuccess = false;
                result.Message = ConstantMessageCodes.FILE_NOT_FOUND;
                result.ResponseCode = ResponseCodeEnum.NOT_FOUND;
                return result;
            }
            catch (NotSupportedException e)
            {
                logger.LogError($"error while getting file with path {filePath} ,error {e}");

                result.IsSuccess = false;
                result.Message = ConstantMessageCodes.FILE_NOT_SUPPORTED;
                result.ResponseCode = ResponseCodeEnum.NOT_SUPPORTED;
                return result;
            }
            catch (Exception e)
            {
                logger.LogError($"error while getting file with path {filePath} ,error {e}");

                result.IsSuccess = false;
                result.Message = ConstantMessageCodes.OPERATION_FAILED;
                result.ResponseCode = ResponseCodeEnum.FAILED;
                return result;
            }
        }
        public OperationResult<bool> Delete(string filePath)
        {
            var result = new OperationResult<bool>
            {
                IsSuccess = true,
                Message = ConstantMessageCodes.OPERATION_SUCCESS,
                ResponseCode = ResponseCodeEnum.SUCCESS,
            };
            try
            {
                var file = new FileInfo(filePath);
                if(file.Exists)
                {
                    file.Delete();
                    return OperationResult.Success(true);
                }
                return OperationResult.Fail(ConstantMessageCodes.FILE_NOT_FOUND, false, ResponseCodeEnum.NOT_FOUND);
               
            }
            
            catch (Exception e)
            {
                logger.LogError($"error while deleting file with path {filePath} ,error {e}");

                result.IsSuccess = false;
                result.Message = ConstantMessageCodes.OPERATION_FAILED;
                result.ResponseCode = ResponseCodeEnum.FAILED;
                return result;
            }
        }
    }

    public class FileManagerResult
    {
        public byte[] Content { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public string FullPath { get; set; }
    }
}