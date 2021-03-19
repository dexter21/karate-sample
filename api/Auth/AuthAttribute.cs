using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace api.Auth
{
    public class AuthAttribute : Attribute, IAuthorizationFilter
    {
        public const string AuthorizationHeader = "Authorization";

        private readonly ITokenStorage _tokenStorage;

        public AuthAttribute(ITokenStorage tokenStorage)
            => _tokenStorage = tokenStorage;

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var header = context.HttpContext.Request.Headers[AuthorizationHeader];
            var token = header.Count > 0 ? header[0] : null;
            if (string.IsNullOrEmpty(token) || !_tokenStorage.Validate(token))
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}