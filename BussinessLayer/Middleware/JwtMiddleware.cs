using System.Security.Claims;
using BussinessLayer.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace BussinessLayer.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IJwtService _jwtService;
        private readonly ILogger<JwtMiddleware> _logger;

        public JwtMiddleware(RequestDelegate next, IJwtService jwtService, ILogger<JwtMiddleware> logger)
        {
            _next = next;
            _jwtService = jwtService;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var bearerToken = context.Request.Cookies.FirstOrDefault(cookie => cookie.Key == "Token").Value;

            if (!string.IsNullOrEmpty(bearerToken))
            {
                var token = bearerToken.Split(" ").Last();
                if (_jwtService.ValidateToken(token))
                {
                    var claims = _jwtService.GetClaimsFromToken(token);
                    
                    if (claims != null)
                    {
                        context.User = claims;
                    }
                }
                else
                {
                    _logger.LogWarning("Invalid JWT token detected.");
                }
            }

            await _next(context);
        }
    }
}