using System.ComponentModel.DataAnnotations;

namespace empty2802core.Entities
{
    public class Users
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Введите имя пользователя")]
        public string Name { get; set; } = null!;
        [EmailAddress]
        [Required(ErrorMessage = "Введите почту")]
        public string Email { get; set; } = null!;
        [Required(ErrorMessage = "Введите пароль")]
        public string Password { get; set; } = null!;
    }
}
