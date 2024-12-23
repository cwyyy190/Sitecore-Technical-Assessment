using System;

namespace Palindrome
{
    class Palindrome
    {
        static void Main()
        {
            Console.Write("InputString: ");
            string inputString = Console.ReadLine();

            string trashSymbolsString = DetectTrashSymbols(inputString);

            Console.WriteLine($"TrashSymbolsString: {trashSymbolsString}");

            Console.WriteLine("Is Palindrome?: " + IsPalindrome(inputString, trashSymbolsString));
            
        }

        static string DetectTrashSymbols(string input)
        {
            return new string(input.Where(c => !char.IsLetterOrDigit(c)).Distinct().ToArray());
        }

        static bool IsPalindrome(string inputString, string trashSymbolsString)
        {
            int left = 0;
            int right = inputString.Length - 1;

            while (left < right)
            {
                while (left < right && (trashSymbolsString.Contains(inputString[left]) || !char.IsLetterOrDigit(inputString[left])))
                {
                    left++;
                }
                while (left < right && (trashSymbolsString.Contains(inputString[right]) || !char.IsLetterOrDigit(inputString[right])))
                {
                    right--;
                }

                if (left < right)
                {
                    if (char.ToLower(inputString[left]) != char.ToLower(inputString[right]))
                    {
                        return false;
                    }

                    left++;
                    right--;
                }
            }

            return true;
        }
    }
}