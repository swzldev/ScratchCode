using System.Security.Cryptography;
using System.Text;

namespace ScratchCodeCompiler.Scratch
{
    internal static class IdGenerator
    {
        public static string GenerateRandomId(int length = 20, bool useGrammar = true)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789;=-#~?";
            const string charsNoGrammar = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            StringBuilder result = new StringBuilder(length);
            using (var rng = RandomNumberGenerator.Create())
            {
                byte[] uintBuffer = new byte[sizeof(uint)];

                while (length-- > 0)
                {
                    rng.GetBytes(uintBuffer);
                    uint num = BitConverter.ToUInt32(uintBuffer, 0);
                    if (useGrammar)
                    {
                        result.Append(charsNoGrammar[(int)(num % (uint)charsNoGrammar.Length)]);
                    }
                    else
                    {
                        result.Append(chars[(int)(num % (uint)chars.Length)]);
                    }
                }
            }
            return result.ToString();
        }
    }
}
