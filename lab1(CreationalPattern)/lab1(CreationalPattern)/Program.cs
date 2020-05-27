using System;

namespace lab1_CreationalPattern_
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter name of the input file: ");
            string startFileName = Console.ReadLine();
            Console.Write("Enter name of the output file: ");
            string endFileName = Console.ReadLine();
         
            while (true)
            {
                Console.WriteLine("Translate text to?");
                Console.WriteLine("1 - HTML");
                Console.WriteLine("2 - Markdown");
                int choose = int.Parse(Console.ReadLine());
                switch (choose)
                {
                    case 1:
                        {
                            clientCode(new HtmlCreator(startFileName, endFileName));
                            return;
                        }
                    case 2:
                        {
                            clientCode(new MarkdownCreator(startFileName, endFileName));
                            return;
                        }
                    default:
                        {
                            Console.WriteLine("Incorrect input. Try again.");
                            continue;
                        }
                }
            }        
        }
        static void clientCode(TranslatorCreator creator)
        {
            creator.makeTranslation();
        }
    }
    
}
