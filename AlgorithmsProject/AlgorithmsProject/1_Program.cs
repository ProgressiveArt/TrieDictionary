using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace AlgorithmsProject
{
    class Program
    {
        public static PrefixTree<string> _tree;
        private static void Main(string[] args)
        {
            _tree = new PrefixTree<string>();
            if (File.Exists("../../DictionaryFIN.txt"))
            {
                using (StreamReader sr = new StreamReader("../../DictionaryFIN.txt", Encoding.Default))
                {
                    string line;
                    int count = 0;
                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();
                    while ((line = sr.ReadLine()) != null)
                    {

                        string[] splitted = line.ToLower().Split('=');
                        string key = splitted[0];
                        string value = splitted[1];
                        _tree.Add(key, value);
                        count++;
                    }
                    stopwatch.Stop();
                    Console.WriteLine("Число слов словаре = " + count + " добавлено в словарь за " + stopwatch.ElapsedMilliseconds + " ms");
                }
            }
            else
            {
                Console.WriteLine("No such file exists");
                Console.ReadKey();
            }

            Console.WriteLine("Вы можете использовать команды : ");
            Console.WriteLine("[1-x-x] - Добавление перевода");
            Console.WriteLine("[2-x] - Удаление перевода");
            Console.WriteLine("[3-x] - Перевод слова");

            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("Введите индекс команды (0 для выхода) : ");

            string command = Console.ReadLine().ToLower();

            while (command != "0")
            {
                try
                {
                    int commandIndex = 0;
                    string value = "";
                    string key = "";
                    if (ParseAndExecute.TryParseCommand(command, ref commandIndex, ref key, ref value))
                    {
                        ParseAndExecute.ExecuteCommand(commandIndex, key, value, _tree);
                    }
                    Console.WriteLine();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ошибка : " + ex.Message);
                    Console.WriteLine();
                }
                finally
                {
                    command = Console.ReadLine();
                }
            }
        }
    }
}
