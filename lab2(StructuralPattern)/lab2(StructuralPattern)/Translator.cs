using System;
using System.IO;
using System.Net;
using System.Web;
using System.Text.RegularExpressions;

namespace lab2_StructuralPattern_
{
    interface ITranslator
    {
        public string translating(string word);
    }
    class GoogleTranslatorEnToUa : ITranslator
    {
        private WebClient m_webClient;
        public GoogleTranslatorEnToUa()
        {
            m_webClient = new WebClient
            {
                Encoding = System.Text.Encoding.UTF8
            };
        }
        public string translating(string word)
        {
            word = word.ToLower();
            string toLanguage = "uk";
            string fromLanguage = "en";
            string url = $"https://translate.googleapis.com/translate_a/single?client=gtx&sl={fromLanguage}&tl={toLanguage}&dt=t&q={HttpUtility.UrlEncode(word)}";
           
            string result = m_webClient.DownloadString(url);
            try
            {
                result = result.Substring(4, result.IndexOf("\"", 4, StringComparison.Ordinal) - 4);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Исключение: {ex.Message}");
                Console.WriteLine($"Метод: {ex.TargetSite}");
                Console.WriteLine($"Трассировка стека: {ex.StackTrace}");
            }
            if(!Regex.IsMatch(result, @"^[a-zA-z]"))
                return result.ToLower();
            return "Такого слова немає";
        }
    }
    class DictionaryEnToUa : ITranslator
    {
        public string translating(string word)
        {
            word = word.ToLower();
            try
            {
                using (StreamReader fs = new StreamReader("dictionary.txt"))
                {
                    string line;
                    while ((line = fs.ReadLine()) != null)
                    {
                        if (line.Substring(0, line.IndexOf(' ')) == word )
                        {
                            return line.Substring(line.LastIndexOf(' ') + 1);
                        }
                    }
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"Исключение: {ex.Message}");
                Console.WriteLine($"Метод: {ex.TargetSite}");
                Console.WriteLine($"Трассировка стека: {ex.StackTrace}");
            }
            return "Такого слова немає в словнику";
        }
    }
    //class DictionaryEnToUa : ITranslator
    //{
    //    public Dictionary<string, string> m_MyDictionary { get; private set; }
    //    public DictionaryEnToUa()
    //    {
    //        m_MyDictionary = new Dictionary<string, string>();
    //        try
    //        {
    //            using (StreamReader fs = new StreamReader("dictionary.txt"))
    //            {
    //                string line;
    //                while ((line = fs.ReadLine()) != null)
    //                {
    //                    string[] words = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
    //                    m_MyDictionary.Add(words[0], words[1]);
    //                }
    //            }
    //        }
    //        catch (FileNotFoundException ex)
    //        {
    //            Console.WriteLine($"Исключение: {ex.Message}");
    //            Console.WriteLine($"Метод: {ex.TargetSite}");
    //            Console.WriteLine($"Трассировка стека: {ex.StackTrace}");
    //        }
    //    }
    //    public string translating(string word)
    //    {
    //        if (m_MyDictionary.TryGetValue(word.ToLower(), out string value))
    //        {
    //            return value;
    //        }
    //        else
    //        {
    //            return "Такого слова немає в словнику";
    //        }
    //    }
    //}
}
