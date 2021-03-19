using System;
using api.Auth;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ITokenStorage _tokenStorage;

        public AuthController(ITokenStorage tokenStorage)
            => _tokenStorage = tokenStorage;

        [HttpPost]
        public IActionResult Login([FromBody] LoginModel model)
        {
            if (model.Login != "admin" || model.Password != "!Q2w3e4r")
            {
                return BadRequest();
            }
            var token = Guid.NewGuid().ToString("D");
            _tokenStorage.Add(token);
            Response.Headers.Add(AuthAttribute.AuthorizationHeader, token);
            return Ok();
        }
        
        [HttpDelete]
        public IActionResult Logout([FromQuery] int id)
        {
            var token = Request.Headers[AuthAttribute.AuthorizationHeader];
            _tokenStorage.Remove(token);
            Response.Headers.Remove(AuthAttribute.AuthorizationHeader);
            return NoContent();
        }
    }
}