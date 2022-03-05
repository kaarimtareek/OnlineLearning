using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using OnlineLearning.Services;

namespace OnlineLearning.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IFileManager fileManager;

        public FilesController(IFileManager fileManager)
        {
            this.fileManager = fileManager;
        }
       
        [HttpGet("/{path}")]
      
        public async Task<IActionResult> GetFile(string path)
        {
            var result = await fileManager.Get(path);

            if(result.IsSuccess)
            {
                return File(result.Data.Content,result.Data.ContentType);
            }
            return BadRequest(result.Message);
        }
    }
}
