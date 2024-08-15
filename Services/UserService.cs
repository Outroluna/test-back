using System.Runtime.ConstrainedExecution;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using test_back.Data;
using test_back.Models;

namespace test_back.Services
{
    public class UserService : IUserService
    {
        // Зависимости для управления пользователями и аутентификацией
        private readonly UserManager<User> UserManager;
        private readonly SignInManager<User> SignInManager;

        // Конструктор, который получает зависимости через внедрение зависимостей (Dependency Injection)
        public UserService(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            UserManager = userManager; // Инициализация менеджера пользователей
            SignInManager = signInManager; // Инициализация менеджера входа
        }

        // Метод для регистрации нового пользователя
        public async Task<bool> RegisterUserAsync(Register model)
        {
            // Создаем нового пользователя на основе данных из модели регистрации
            var user = new User
            {
                UserName = model.Name, // Устанавливаем имя пользователя
                Email = model.Email // Устанавливаем email пользователя
            };

            // Пытаемся создать пользователя с указанным паролем
            var result = await UserManager.CreateAsync(user, model.Password);

            // Если создание пользователя прошло успешно
            if (result.Succeeded)
            {
                // Выполняем автоматический вход после регистрации
                await SignInManager.SignInAsync(user, isPersistent: false);
                return true; // Возвращаем успех
            }

            return false; // Возвращаем неудачу в случае ошибки
        }

        // Метод для входа пользователя
        public async Task<bool> LoginUserAsync(Login model)
        {
            // Пытаемся выполнить вход пользователя по name и паролю
            var result = await SignInManager.PasswordSignInAsync(model.Name, model.Password, model.RememberMe, lockoutOnFailure: false);

            return result.Succeeded; // Возвращаем результат входа (успешно или нет)
        }
        // Метод для выхода пользователя из системы
        public async Task LogoutUserAsync()
        {
            await SignInManager.SignOutAsync(); // Выполняем выход пользователя
        }

        // Метод для получения пользователя по его уникальному идентификатору
        public async Task<User> GetUserByIdAsync(string userId)
        {
            return await UserManager.FindByIdAsync(userId); // Ищем пользователя по ID
        }

        // Метод для получения списка ролей пользователя
        public async Task<IList<string>> GetUserRolesAsync(User user)
        {
            return await UserManager.GetRolesAsync(user); // Получаем роли, связанные с пользователем
        }
    }
}





