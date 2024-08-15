using test_back.Models;
namespace test_back.Services
{
    public interface IUserService
    {
        // Метод для регистрации пользователя
        Task<bool> RegisterUserAsync(Register model);

        // Метод для входа пользователя
        Task<bool> LoginUserAsync(Login model);

        // Метод для выхода пользователя
        Task LogoutUserAsync();

        // Метод для получения пользователя по его уникальному идентификатору
        Task<User> GetUserByIdAsync(string userId);

        // Метод для получения списка ролей пользователя
        Task<IList<string>> GetUserRolesAsync(User user);
    }
}
