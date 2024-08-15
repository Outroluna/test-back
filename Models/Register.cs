using System.ComponentModel.DataAnnotations;

namespace test_back.Models
{
    public class Register ///Сбор и валидация данных
    {
        [Required] ///Атрибут, обязательно для заполнения
        [EmailAddress] ///Должен быть введен email
        public string? Email { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Address { get; set; }

        [Required]
        [DataType(DataType.Password)] ///Поле для ввода пароля
        public string? Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")] ///Совпадение паролей
        public string? ConfirmPassword { get; set; }

        [Phone]
        public string? PhoneNumber { get; set; }
    }
}
