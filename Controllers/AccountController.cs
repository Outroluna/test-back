using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using test_back.Models;

namespace test_back.Controllers
{
    public class AccountController: Controller
    {
        private readonly UserManager<IdentityUser> UserManager; ///Управление пользователями
        private readonly SignInManager<IdentityUser> SignInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Register() => View();///озвращает представление для страницы регистрации ?????????

        [HttpPost]
        public async Task<IActionResult> Register(Register model)///Регистрация пользователя
        {
            if (ModelState.IsValid) ///Проверка на валидность
            {
                var user = new IdentityUser { UserName = model.Email, Email = model.Email }; ///Создание нового пользователя на основе данных регистрации
                var result = await UserManager.CreateAsync(user, model.Password); ///Создание пользователя с паролем
                if (result.Succeeded) ///Проверка, успешно ли создан пользователь
                {
                    await SignInManager.SignInAsync(user, isPersistent: false); /// Если пользователь успешно создан, выполняем вход
                    return RedirectToAction(""); ///Перенаправление на главную страницу после успешной регистрации
                }
                foreach (var error in result.Errors) ///Если есть ошибки при создании пользователя
                {
                    ModelState.AddModelError(string.Empty, error.Description);///Добавляем каждую ошибку в состояние модели для отображения на странице
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(Login model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    return RedirectToAction("");
                }

                ModelState.AddModelError("","Неправильный логин или пароль");
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await SignInManager.SignOutAsync();///Выход пользователя
            return RedirectToAction("");//Перенаправление на главную страницу после выхода
        }

        ///Метод безопасного перенаправления на стр после дейсттвий (спросить у Сереги еще раз о методе)
        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("");
            }
        }
    }
}
