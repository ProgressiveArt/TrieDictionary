using System;
using System.Text.RegularExpressions;


namespace AlgorithmsProject
{
    class InputCheck
    {
        public static void KeyCheck(string key)
        {
            Regex pattern = new Regex(@"([a-z ]+)");
            Match match = pattern.Match(key);

            if (!match.Success)
            {
                throw new ArgumentException(@"Ключ должен быть словом\буквой английского алфавита(может содержать пробелы)");
            }
        }

        public static void ValueCheck(string value)
        {
            Regex pattern = new Regex(@"([а-я ]+)");
            Match match = pattern.Match(value);

            if (!match.Success)
            {
                throw new ArgumentException(@"Перевод должен быть словом\словосочетание\буквой русского алфавита(может содержать пробелы)");
            }
        }

        public static void keySymbolsCheck(string key)
        {
            Regex pattern = new Regex(@"([^a-z ]+)");
            Match match = pattern.Match(key);

            if (match.Success)
            {
                throw new ArgumentException(@"Ключ не может содержать символы\цифры");
            }
        }

        public static void ValueSymbolsCheck(string value)
        {
            Regex pattern = new Regex(@"([^а-я ]+)");
            Match match = pattern.Match(value);

            if (match.Success)
            {
                throw new ArgumentException(@"Перевод не может содержать символы\цифры");
            }
        }
    }
}
