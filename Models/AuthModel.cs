using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Beehive.Models
{
    public class AuthModel
    {
        static readonly private int[] nums = [7, 127, 61, 211, 13];

        [EmailAddress]
        [Display(Name = "e-mail")]
        public string Email { get; set; } = null!;

        [MinLength(2)]
        [MaxLength(64)]
        [Display(Name = "Ник")]
        public string? Name { get; set; }

        [MinLength(8)]
        [MaxLength(32)]
        [Display(Name = "Пароль")]
        public string Password { get; set; } = null!;

        [Display(Name = "Пароль")]
        public string? RepeatPassword { get; set; }

        public int PasswordHash => EncryptPassword();

        public bool ArePasswordsEqual => StringComparer.Ordinal.Equals(Password, RepeatPassword);

        int EncryptPassword()
        {
            var res = 13;
            for (int i = 0; i != Password.Length; i++)
                res += Password[i] * nums[i % 5];
            return res;
        }
    }
}
