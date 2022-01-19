using System.Threading.Tasks;

namespace GameOfChance.Client.Services
{
    public interface IUserManager
    {

        Task<UserManagerResponse> RegisterUserAsync(RegisterModel model);
        Task<UserManagerResponse> LoginUserAsync(LoginModel model);
        Task<UserManagerResponse> SignOutUserAsync();
    }
}
