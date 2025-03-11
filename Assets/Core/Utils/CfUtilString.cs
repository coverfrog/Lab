using System.Text.RegularExpressions;
using UnityEngine;

namespace Cf.Utils
{
    public static partial class CfUtil 
    {
        public static class String
        {
            public static string ToPascal(string input)
            {
                string[] words = Regex.Split(input, @"[^a-zA-Z0-9]+");
            
                for (int i = 0; i < words.Length; i++)
                {
                    if (words[i].Length <= 0)
                    {
                        continue;
                    }

                    string word = words[i];

                    words[i] = char.ToUpper(word[0]) + word[1..];
                }
            
                return string.Join("", words);
            }
        }
    }
}
