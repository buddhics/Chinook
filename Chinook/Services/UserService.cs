using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace Chinook.Services
{
    public class UserService : IUserService
    {
        public async Task<string> GetUserIdAsync(Task<AuthenticationState> authenticationState)
        {
            var user = (await authenticationState).User;
            return user.FindFirst(u => u.Type.Contains(ClaimTypes.NameIdentifier))?.Value ?? string.Empty;
        }
    }
}
