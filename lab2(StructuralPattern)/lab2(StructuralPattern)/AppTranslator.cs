using System;
using System.IO;

namespace lab2_StructuralPattern_
{
    class AppTranslator
    {
        private ITranslator m_translator;
        public AppTranslator(ITranslator translator)
        {
            m_translator = translator;            
        }
        
        public string translateWord(string word)
        {
            return m_translator.translating(word);
        }
        public void addWordToDictionary(string wordEn, string wordUa)
        {
            wordEn = wordEn.ToLower();
            wordUa = wordUa.ToLower();
            try
            {
                using (StreamWriter fp = new StreamWriter("dictionary.txt", true))
                {
                    fp.WriteLine(wordEn + " " + wordUa);
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"Исключение: {ex.Message}");
                Console.WriteLine($"Метод: {ex.TargetSite}");
                Console.WriteLine($"Трассировка стека: {ex.StackTrace}");
            } 
        }
        public void showDictionary()
        {
            try
            {
                using (StreamReader fs = new StreamReader("dictionary.txt"))
                {
                    string line;
                    int i = 1;
                    while ((line = fs.ReadLine()) != null)
                    {
                        Console.WriteLine($"{i++}). {line}");
                    }
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"Исключение: {ex.Message}");
                Console.WriteLine($"Метод: {ex.TargetSite}");
                Console.WriteLine($"Трассировка стека: {ex.StackTrace}");
            }
        }
    }
}
