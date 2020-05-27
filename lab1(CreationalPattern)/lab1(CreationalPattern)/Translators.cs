using System;
using System.IO;

namespace lab1_CreationalPattern_
{
    public interface ITranslator
    {
        public void translating(string startFileName, string endFileName);
    }

    class Html : ITranslator
    {
        public void translating(string startFileName, string endFileName)
        {
            Console.WriteLine("HTML");
            try
            {
                using (StreamReader fs = new StreamReader(startFileName))
                {
                    using (StreamWriter fp = new StreamWriter(endFileName, false))
                    {
                        string line;
                        while ((line = fs.ReadLine()) != null)
                        {
                            if (line == "")
                            {
                                fp.WriteLine("");
                                continue;
                            }

                            int pos = line.IndexOf(' ');
                            string checkLine;
                            if (pos == -1)
                                checkLine = line; // строка состоит с одного слова
                            else
                                checkLine = line.Substring(0, pos);

                            switch (checkLine)
                            {
                                case "p":
                                    {
                                        fp.WriteLine("<p>" + line.Substring(pos, line.Length - pos) + " </p>");
                                        break;
                                    }
                                case "h1":
                                    {
                                        fp.WriteLine("<h1>" + line.Substring(pos, line.Length - pos) + " </h1>");
                                        break;
                                    }
                                case "h2":
                                    {
                                        fp.WriteLine("<h2>" + line.Substring(pos, line.Length - pos) + " </h2>");
                                        break;
                                    }
                                case "h3":
                                    {
                                        fp.WriteLine("<h3>" + line.Substring(pos, line.Length - pos) + " </h3>");
                                        break;
                                    }
                                case "ordlist":
                                    {
                                        fp.WriteLine("<ol>");
                                        line = fs.ReadLine();

                                        while ((line = fs.ReadLine()) != "" && line != null)
                                        {
                                            fp.WriteLine("  <li> " + line + " </li>");
                                        }

                                        fp.WriteLine("</ol>");
                                        fp.WriteLine("");
                                        break;
                                    }
                                case "bullist":
                                    {
                                        fp.WriteLine("<ul>");
                                        line = fs.ReadLine();

                                        while ((line = fs.ReadLine()) != "" && line != null)
                                        {
                                            fp.WriteLine("  <li> " + line + " </li>");
                                        }

                                        fp.WriteLine("</ul>");
                                        fp.WriteLine("");
                                        break;
                                    }
                                    //default:
                                    //    {
                                    //        fp.WriteLine("");
                                    //        break;
                                    //    }
                            }
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
        }
    }
    class Markdown : ITranslator
    {
        public void translating(string startFileName, string endFileName)
        {
            Console.WriteLine("Markdown");
            try
            {
                using (StreamReader fs = new StreamReader(startFileName))
                {
                    using (StreamWriter fp = new StreamWriter(endFileName, false))
                    {
                        string line;
                        while ((line = fs.ReadLine()) != null)
                        {
                            if (line == "")
                            {
                                fp.WriteLine("");
                                continue;
                            }

                            int pos = line.IndexOf(' ');
                            string checkLine;
                            if (pos == -1)
                                checkLine = line;
                            else
                                checkLine = line.Substring(0, pos);

                            switch (checkLine)
                            {
                                case "p":
                                    {
                                        fp.WriteLine(line.Substring(pos, line.Length - pos));
                                        break;
                                    }
                                case "h1":
                                    {
                                        fp.WriteLine("#" + line.Substring(pos, line.Length - pos));
                                        break;
                                    }
                                case "h2":
                                    {
                                        fp.WriteLine("##" + line.Substring(pos, line.Length - pos));
                                        break;
                                    }
                                case "h3":
                                    {
                                        fp.WriteLine("###" + line.Substring(pos, line.Length - pos));
                                        break;
                                    }
                                case "ordlist":
                                    {
                                        line = fs.ReadLine();
                                        int num = 1;

                                        while ((line = fs.ReadLine()) != "" && line != null)
                                        {
                                            fp.WriteLine(num.ToString() + ". " + line);
                                            ++num;
                                        }

                                        fp.WriteLine("");
                                        break;
                                    }
                                case "bullist":
                                    {
                                        line = fs.ReadLine();

                                        while ((line = fs.ReadLine()) != "" && line != null)
                                        {
                                            fp.WriteLine("- " + line);
                                        }

                                        fp.WriteLine("");
                                        break;
                                    }
                                    //default:
                                    //    {
                                    //        fp.WriteLine("");
                                    //        break;
                                    //    }
                            }
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
        }
    }
}
