﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace OneToManyMapBenchmark
{
    internal static class Randomizer
    {
        private static int[] s_punctuationACIICodes = { 33, 35, 36, 37, 38, 40, 41, 42, 43, 44, 45, 46, 47, 58, 59, 60, 61, 62, 63, 64, 91, 92, 93, 94, 95, 123, 124, 125, 126 };


        private static RNGCryptoServiceProvider rngCrypto = new RNGCryptoServiceProvider();
        private static int GetRandomNumber()
        {
            var buffer = new byte[4];
            rngCrypto.GetBytes(buffer);
            return BitConverter.ToInt32(buffer, 0);
        }

        public static int[] GetRandomUniqueInts(int numberOfElements)
        {
            var hashset = new HashSet<int>();
            var randomInts = new int[numberOfElements];

            var generated = 0;
            do
            {
                var number = GetRandomNumber();
                if (!hashset.Contains(number))
                {
                    randomInts[generated] = number;
                    hashset.Add(number);
                    generated++;
                }
            }
            while (generated < numberOfElements);

            return randomInts;
        }

        public static T[] ShuffleArray<T>(T[] source)
        {
            var keyValuePairs = new List<KeyValuePair<int, T>>();
            var randomUniqueInts = GetRandomUniqueInts(source.Length);

            for (int i = 0; i < source.Length; i++)
            {
                keyValuePairs.Add(new KeyValuePair<int, T>(randomUniqueInts[i], source[i]));
            }

            return keyValuePairs.OrderBy(kvp => kvp.Key).Select(kvp => kvp.Value).ToArray();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow", MessageId = "length-4")]
        public static string GetRandomAciiString(int length)
        {
            const int NUMERIC = 0;
            const int LOWERCASE = 1;
            const int UPPERCASE = 2;
            const int PUNCT = 3;
            var random = new Random(GetRandomNumber());

            StringBuilder sb = new StringBuilder();

            //ensure at least one of each type occurs 
            sb.Append(Convert.ToChar(random.Next(97, 122))); //lowercase
            sb.Append(Convert.ToChar(random.Next(65, 90))); //uppercase
            sb.Append(Convert.ToChar(random.Next(48, 57))); //numeric
            sb.Append(Convert.ToChar(s_punctuationACIICodes[random.Next(0, s_punctuationACIICodes.Length)])); //punctuation

            char ch;
            for (int i = 0; i < length - 4; i++)
            {
                int rnd = random.Next(0, 3);
                switch (rnd)
                {
                    case LOWERCASE: ch = Convert.ToChar(random.Next(97, 122)); break;
                    case UPPERCASE: ch = Convert.ToChar(random.Next(65, 90)); break;
                    case NUMERIC: ch = Convert.ToChar(random.Next(48, 57)); break;
                    case PUNCT: ch = Convert.ToChar(s_punctuationACIICodes[random.Next(0, s_punctuationACIICodes.Length)]); break;
                    default:
                        ch = Convert.ToChar(random.Next(97, 122)); break;
                }
                sb.Append(ch);
            }
            return sb.ToString();
        }

        public static string[] GetRandomUniqueAciiStrings(int length, int numberOfElements)
        {
            var hashset = new HashSet<string>();
            var randomStrings = new string[numberOfElements];

            var generated = 0;
            do
            {
                var data = GetRandomAciiString(length);
                if (!hashset.Contains(data))
                {
                    randomStrings[generated] = data;
                    hashset.Add(data);
                    generated++;
                }
            }
            while (generated < numberOfElements);

            return randomStrings;
        }

        public static string GetRandomUnicodeString(int length)
        {
            var random = new Random(GetRandomNumber());
            length = length * 2;

            byte[] str = new byte[length];

            for (int i = 0; i < length; i += 2)
            {
                int chr = random.Next(0xD7FF);
                str[i + 1] = (byte)((chr & 0xFF00) >> 8);
                str[i] = (byte)(chr & 0xFF);
            }

            return Encoding.Unicode.GetString(str);
        }
    }
}
