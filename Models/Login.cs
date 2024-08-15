using System.ComponentModel.DataAnnotations;

namespace test_back.Models
{
    public class Login ///Сбор данных, введенных пользователем
    {
        [Required]
        public string? Name { get; set; }


        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Display(Name = "Запомнить вас?")]
        public bool RememberMe { get; set; }
    }
}
