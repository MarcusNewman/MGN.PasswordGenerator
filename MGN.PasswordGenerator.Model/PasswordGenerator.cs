using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace MGN.PasswordGenerator.Model
{
    public class PasswordGenerator
    {
        public static Random random = InitRandom();
        public static String GeneratePassword(int length = 15, Boolean useFiller = true)
        {
            var result = new char[length];
            var filler = GetRandomChar(All);

            AddCharacter(LowerCase, filler, result);
            AddCharacter(UpperCase, filler, result);
            AddCharacter(Numbers, filler, result);
            AddCharacter(Special, filler, result);

            //fill the rest of the password with the filler character
            for (int i = 0; i < result.Length; i++)
            {
                if (result[i] == '\0') result[i] = filler;
            }
            return new string(result);
        }

        private static char GetRandomChar(string alphabet)
        {
            return alphabet[random.Next(alphabet.Length)];
        }

        private static void AddCharacter(string alphabet, char filler, char[] result)
        {
            var character = GetRandomChar(alphabet);
            //Ensure a unique filler
            while (character == filler) character = GetRandomChar(alphabet);
            var nextEmptyPosition = random.Next(result.Length);
            while (result[nextEmptyPosition] != '\0')
            {
                nextEmptyPosition = random.Next(result.Length);
            }
            result[nextEmptyPosition] = character;
        }

        private static Random InitRandom()
        {
            // From http://www.obviex.com/Samples/Password.aspx
            byte[] randomBytes = new byte[4];
            // Generate 4 random bytes.
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(randomBytes);

            // Convert 4 bytes into a 32-bit integer value.
            int seed = (randomBytes[0] & 0x7f) << 24 |
                    randomBytes[1] << 16 |
                    randomBytes[2] << 8 |
                    randomBytes[3];
            return new Random(seed);
        }

        public const string LowerCase = "abcdefghijklmnopqrstuvwxyz";
        public const string UpperCase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public const string Numbers = "0123456789";
        public const string Special = " !\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~";
        public const string All = LowerCase + UpperCase + Numbers + Special;
    }
}

