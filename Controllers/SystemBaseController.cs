using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using OnlineLearning.Constants;

using System;
using System.Linq;

namespace OnlineLearning.Controllers
{
   // [Route("api/[controller]")]
    [ApiController]
    public class SystemBaseController : ControllerBase
    {
        public string UserId
        {
            get
            {
                var userId = HttpContext.User.Claims
                        .FirstOrDefault(c => c.Type == ConstantSecurityHeaders.Id)?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    throw new ArgumentNullException("UserId cannot be null");
                }
                return userId;
            }
        }
    }
}