using Beehive.Controllers;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security;
using System.Security.Cryptography;

namespace Beehive.Models
{
    public class AuthModel()
    {
        static readonly protected internal int[] nums = [7, 127, 61, 211, 13, 727, 311, 457, 607];

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

        //public byte[] Pbkdf2 => EncryptPassword();

        public bool ArePasswordsEqual => StringComparer.Ordinal.Equals(Password, RepeatPassword);

        protected internal virtual byte[] EncryptPassword()
        {
            byte[] salt = AuthorizeController.GetSalt(Email);
            return Rfc2898DeriveBytes.Pbkdf2(Password, salt, 210000, HashAlgorithmName.SHA256, 32);
        }
    }
}
