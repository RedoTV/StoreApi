using AuthApi.Models;

namespace AuthApi.Services.Interfaces
{
    public interface IIdentityService
    {
        public string GetToken(User user);
        public User? GetUser(UserFormInfo userFormInfo);
        public string? RegisterUser(UserFormInfo userFormInfo);
    }
}