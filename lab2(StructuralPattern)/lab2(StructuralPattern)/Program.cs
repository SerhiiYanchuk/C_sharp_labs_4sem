using System;

namespace lab2_StructuralPattern_
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Default;
            int choose;
            while (true)
            {
                Console.WriteLine("В якому режимі ви працюєте?");
                Console.WriteLine("1 - Онлайн");
                Console.WriteLine("2 - Оффлайн");
                choose = int.Parse(Console.ReadLine());
                switch (choose)
                {
                    case 1:
                        {
                            executeProgram(new AppTranslator(new GoogleTranslatorEnToUa()));
                            return;
                        }
                    case 2:
                        {
                            executeProgram(new AppTranslator(new DictionaryEnToUa()));
                            return;
                        }
                    default:
                        {
                            Console.WriteLine("Incorrect input. Try again.");
                            break;
                        }
                }
            }
        }

        static void executeProgram(AppTranslator appTranslator)
        {
            int choose;
            while (true)
            {
                Console.WriteLine("\n\nПанель управління");
                Console.WriteLine("1 - Перекласти слово");
                Console.WriteLine("2 - Добавити слово у словник");
                Console.WriteLine("3 - Продивитися весь словник");
                Console.WriteLine("0 - Exit");
                choose = int.Parse(Console.ReadLine());
                switch (choose)
                {
                    case 1:
                        {
                            Console.Write("Введіть слово: ");
                            string word = Console.ReadLine();
                            Console.WriteLine("Переклад: " + appTranslator.translateWord(word) + "\n");
                            break;
                        }
                    case 2:
                        {
                            Console.Write("Введіть слово на англійській: ");
                            string wordEn = Console.ReadLine();
                            Console.Write("Введіть слово на українській: ");
                            string wordUa = Console.ReadLine();
                            appTranslator.addWordToDictionary(wordEn, wordUa);
                            Console.WriteLine("Готово\n");
                            break;
                        }
                    case 3:
                        {
                            appTranslator.showDictionary();
                            break;
                        }
                    case 0:
                        {
                            return;
                        }

                    default:
                        {
                            Console.WriteLine("Incorrect input. Try again.\n");
                            continue;
                        }
                }
            }
        }
    }
}
