using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace Beehive
{
    internal static class GlobalVals
    {
        internal const string PRIVATE_KEY_COOKIE_NAME = "Priv_Key";

        private static readonly string path = Path.Combine(Environment.CurrentDirectory, "Passkeys");

        // На практике для этого используются хранилища вроде Azure Key Vault - они все платные
        internal static void WritePasskey(Guid id, byte[] key)
        {
            var f = new FileInfo(path);
            var bid = id.ToByteArray();
            using var s = f.OpenWrite();
            s.Position = s.Length;
            s.Write(bid);
            s.Write([0, 0, 0, 0, 0, 0]);
            s.Write(key);
        }

        internal static byte[]? ReadPassKey(Guid id)
        {
            var f = new FileInfo(path);
            var res = new byte[128];
            var bid = id.ToByteArray();
            var buf = new byte[bid.Length];
            //try
            //{
                using var s = f.OpenRead();
                s.Read(buf);
                while (!Enumerable.SequenceEqual(bid, buf))
                {
                    s.Seek(6 + res.Length, SeekOrigin.Current);
                    if (s.Read(buf) != buf.Length) throw new ArgumentException("GUID-passkey pair not found");
                }
                s.Seek(6, SeekOrigin.Current);
                s.Read(res);
                return res;
            //}
            //catch
            //{
            //    return null;
            //}
        }
    }
}
