using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTaskFujifilm
{
    public static class Palindrome
    {

        /// <summary>
        /// Answers is the given string a palindrome
        /// </summary>
        /// <param name="palindromeString"></param>
        /// <returns>Is </returns>
        public static bool IsPalindrome(string palindromeString)
        {
            if (string.IsNullOrEmpty(palindromeString))
            {
                return false;
            }

            for (int i = 0, ii = palindromeString.Length - 1; i < palindromeString.Length - 1; i++, ii--)
            {

                if (palindromeString[i] != palindromeString[ii]) return false;

            }

            return true;

        }

    }
}
