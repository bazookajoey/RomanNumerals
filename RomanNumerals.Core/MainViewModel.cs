using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;

namespace RomanNumerals.Core
{
    public class MainViewModel
    {
        private readonly Dictionary<int, string> conversionTable = new Dictionary<int, string>
        {
            { 1000, "M" },
            { 900, "CM" },
            { 500, "D" },
            { 400, "CD" },
            { 100, "C" },
            { 90, "XC" },
            { 50, "L" },
            { 40, "XL" },
            { 10, "X" },
            { 9, "IX" },
            { 5, "V" },
            { 4, "IV" },
            { 1, "I" }
        };

        public string RomanNumeral { get; set; }
        public int ArabicNumeral { get; set; }
        public string CurrentError { get; set; }

        public void RomanToArabic()
        {
            var roman = RomanNumeral;
            var arabic = 0;
            CurrentError = "";

            int consecutiveCharacterCount;
            foreach (var c in conversionTable)
            {
                consecutiveCharacterCount = 0;
                while (true)
                {
                    if (roman.StartsWith(c.Value))
                    {
                        consecutiveCharacterCount++;
                        if (consecutiveCharacterCount >= 4)
                        {
                            CurrentError = $"Cannot have more than 4 {c.Value} characters in a row.";
                            arabic = 0;
                            break;
                        }

                        arabic += c.Key;
                        roman = roman.Substring(c.Value.Length);
                    }
                    else
                    {
                        break;
                    }
                }

                if (string.IsNullOrEmpty(roman) || !string.IsNullOrEmpty(CurrentError))
                    break;
            }

            ArabicNumeral = arabic;
        }

        public void ArabicToRoman()
        {
            CurrentError = "";
            string roman = "";

            if (ArabicNumeral < 0)
            {
                CurrentError = "Arabic Number must be a non-negative integer.";
            }
            else if (ArabicNumeral == 0)
            {
                roman = "nulla";
            }
            else if (ArabicNumeral >= 4000)
            {
                CurrentError = "Unable to calculate a value greater than 3,999.";
            }
            else
            {
                var arabic = ArabicNumeral;

                foreach (var c in conversionTable)
                {
                    var characterCount = (arabic - (arabic % c.Key)) / c.Key;

                    for (int i = 0; i < characterCount; i++)
                        roman += c.Value;

                    arabic -= characterCount * c.Key;

                    if (arabic <= 0)
                        break;
                }
            }

            RomanNumeral = roman;
        }
    }
}
