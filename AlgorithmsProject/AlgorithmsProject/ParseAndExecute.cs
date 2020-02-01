using System;

namespace AlgorithmsProject
{
    class ParseAndExecute
    {
        public static bool TryParseCommand(string line, ref int commandIndex, ref string key, ref string value)
        {
            try
            {
                string[] splitted = line.Split('-');

                commandIndex = int.Parse(splitted[0]);

                if (commandIndex < 1 || commandIndex > 3)
                    throw new IndexOutOfRangeException("Индекс команды за пределами диапазона.");

                if (!line.Contains("-"))
                    throw new IndexOutOfRangeException("Неверный формат запроса.");
                else key = splitted[1].Trim(' ');


                if (commandIndex == 1)
                {
                    value = splitted[2].Trim(' ');
                    InputCheck.ValueCheck(value);
                    InputCheck.ValueSymbolsCheck(value);
                }
                else value = null;


                if (commandIndex == 1)
                {
                    if (key == "" || key == null)
                        throw new Exception("Нужно ввести слово, которое хотите добавить.");

                    else if (value == "" || value == null)
                        throw new Exception("Нужно ввести перевод добавляемого слова.");

                    if (splitted.Length > 3)
                    {
                        throw new IndexOutOfRangeException("Неверный формат запроса.");
                    }
                }

                if (commandIndex == 2)
                {
                    if (key == "" || key == null || key == string.Empty)
                        throw new Exception("Нужно ввести слово, которое хотите удалить.");
                    if (splitted.Length > 2)
                    {
                        throw new IndexOutOfRangeException("Неверный формат запроса.");
                    }
                }

                if (commandIndex == 3)
                {
                    if (key == "" || key == null)
                        throw new Exception("Нужно ввести слово, которое хотите перевести.");

                    if (splitted.Length > 2)
                    {
                        throw new IndexOutOfRangeException("Неверный формат запроса.");
                    }
                }

                InputCheck.KeyCheck(key);
                InputCheck.keySymbolsCheck(key);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public static void ExecuteCommand(int commandIndex, string key, string value, PrefixTree<string> _tree)
        {
            if (commandIndex == 1)
            {
                if (_tree.ContainsValue(key))
                {
                    Console.WriteLine("Перевод слова '" + key + "' уже есть в данном словаре.");
                    Console.WriteLine("Хотите перезаписать? Y - да | N - нет");
                    choose:
                    string command = Console.ReadLine().ToLower().Trim(' ');

                    if (command == "y")
                    {
                        _tree.Add(key, value);
                        Console.WriteLine("Перевод слова '" + key + "' изменен на '" + value + "'");
                    }
                    else if (command == "n")
                    {
                        Console.WriteLine("Перезапись отменена");
                    }
                    else
                    {
                        Console.WriteLine("Некоректный ввод");
                        goto choose;
                    }
                }

                else
                {
                    _tree.Add(key, value);
                    Console.WriteLine("В словарь добавлено слово '" + key + "' с переводом - " + value);
                }
            }
        

    
            if (commandIndex == 2)
            {
                if (_tree.ContainsValue(key))
                {
                    _tree.Remove(key);
                    Console.WriteLine("Слово '" + key + "' было удалено из словаря.");
                }
                else
                {
                    Console.WriteLine("Слова '" + key + "' не существует в данном словаре");
                }
            }

            if (commandIndex == 3)
            {
                _tree.GetValue(key);
                Console.WriteLine("Перевод слова: '" + key + "' = " + _tree.GetValue(key));
            }
        }
    }
}
