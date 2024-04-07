using Microsoft.AspNetCore.Components.Authorization;

namespace Chinook.Services
{
    public interface IUserService
    {
        Task<string> GetUserIdAsync(Task<AuthenticationState> authenticationState);
    }
}
