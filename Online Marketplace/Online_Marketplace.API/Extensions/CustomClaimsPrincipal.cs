using System.Security.Claims;

namespace Online_Marketplace.API.Extensions
{
    public static class CustomClaimsPrincipal
    {



        public static string GetUsername(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.Name)?.Value;
        }

        public static string? GetUserId(this ClaimsPrincipal user)
        {
            return user.FindFirstValue("Id");
        }

        public static IEnumerable<string> GetRoles(this ClaimsPrincipal user)
        {
            return user.Claims.Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value);
        }
    }
}