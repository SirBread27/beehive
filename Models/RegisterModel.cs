using System.Security.Cryptography;
using System.Text;

namespace Beehive.Models
{
    public class RegisterModel : AuthModel
    {
        public RegisterModel()
        {
            var rng = new Random();
            var len = rng.Next(16, 21);
            Salt = new byte[len];
            rng.NextBytes(Salt);
        }

        public byte[] Salt { get; set; } = null!;

        protected internal override byte[] EncryptPassword()
        {
            return Rfc2898DeriveBytes.Pbkdf2(Password, Salt, 210000, HashAlgorithmName.SHA256, 32);
        }
    }
}
