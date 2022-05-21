using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

using RestSharp;

using System;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearning.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize]
    public class ZoomController : ControllerBase
    {
        private readonly IConfiguration configuration;

        public ZoomController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [HttpGet("Login")]
        public IActionResult Login()
        {
            var authUrl = configuration["ZoomSettings:AuthorizationUrl"];
            var clientId = configuration["ZoomSettings:clientId"];
            var redirectionUrl = configuration["ZoomSettings:RedirectionUrl"];
            string url = string.Format(authUrl, clientId, redirectionUrl);
            return Redirect(url);
        }

        [HttpGet("oauthRedirect")]
        public async Task<IActionResult> OAuth([FromQuery] string code)
        {
            var accessTokenUrl = configuration["ZoomSettings:AccessTokenUrl"];
            var redirectionUrl = configuration["ZoomSettings:RedirectionUrl"];

            var accessTokenUrlFormatted = string.Format(accessTokenUrl, code, redirectionUrl);
            var client = new RestClient(new Uri(accessTokenUrlFormatted));
            var request = new RestRequest();
            var clientId = configuration["ZoomSettings:clientId"];
            var clientSecret = configuration["ZoomSettings:clientSecret"];
            var encodedHeaer = Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}");
            var encodedString = Convert.ToBase64String(encodedHeaer);
            var authorizationHeader = $"Basic {encodedString}";
            request.AddHeader("Authorization", authorizationHeader);
            var result = await client.PostAsync(request);
            if (!result.IsSuccessful)
            {
                return BadRequest(result.Content);
            }
            return Ok(result.Content);

        }

    }
}
