using Microsoft.IdentityModel.JsonWebTokens;
using Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Authentication
{
    public class ClaimsPrincipalExtensions
    {
        public static string GetUserId(ClaimsPrincipal? principal)
        {
            string? userId = principal?.FindFirstValue(JwtRegisteredClaimNames.Sub);

            return Guid.TryParse(userId, out Guid parsedUserId) ?
                parsedUserId.ToString() :
                throw new ApplicationException("User id is unavailable!");
        }

        public static RoleEnum GetUserRole(ClaimsPrincipal? principal)
        {
            string userRole = principal?.FindFirstValue(ClaimTypes.Role);

            return Enum.TryParse<RoleEnum>(userRole, out var parsedRole)
               ? parsedRole
               : throw new ApplicationException("User role is unavailable or invalid!");
        }
    }
}
