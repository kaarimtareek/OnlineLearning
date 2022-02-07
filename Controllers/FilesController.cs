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
        [HttpPost]
        //[Consumes("*/*")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> AddFile(
            //[FromBody] IFormFile file2
            )
        {
            var file = HttpContext.Request.Form.Files.FirstOrDefault();
            var result2 = await fileManager.Add(file, "Test2");
            if(result2.IsSuccess)
            {
                return Ok(result2.Data);
            }
            return BadRequest(result2.Message);
        }
        [HttpGet("/{path}")]
        //[Consumes("*/*")]
        public async Task<IActionResult> AddFile(
            //[FromBody] IFormFile file2
            string path
            )
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
