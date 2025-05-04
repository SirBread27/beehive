using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;

namespace Beehive
{
    internal static class GlobalVals
    {
        internal const string PRIVATE_KEY_COOKIE_NAME = "Priv_Key";

        internal static Dictionary<Guid, byte[]> Passwords = [];
    }
}
