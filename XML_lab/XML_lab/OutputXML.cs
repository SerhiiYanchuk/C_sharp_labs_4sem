using System;
using System.Xml;

namespace XML_lab
{
    partial class Program
    {
        static public void ReadProductsXML()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("products.xml");
            int count = 0;
            foreach (XmlNode product in doc.DocumentElement.ChildNodes)
            {
                string id = product["id"].InnerText;
                string name = product["name"].InnerText;
                string calorise = product["calories"].InnerText;
                Console.WriteLine($"{++count}).");
                Console.WriteLine(string.Format(" Id = {0}\n продукт = {1}\n каллорий на 100г = {2}", id, name, calorise));
            }
            Console.ReadKey();
        }
        static public void ReadDishesXML()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("dishes.xml");
            int count = 0;
            foreach (XmlNode dish in doc.DocumentElement.ChildNodes)
            {
                string id = dish["id"].InnerText;
                string name = dish["name"].InnerText;
                Console.WriteLine($"{++count}).");
                Console.WriteLine(string.Format(" Id = {0}\n блюдо = {1}\n Продукты:", id, name));
                foreach (XmlNode product in dish["products"].ChildNodes)
                {
                    string productId = product["productId"].InnerText;
                    string quantity = product["quantity"].InnerText;
                    Console.WriteLine(string.Format("   Id = {0}, количество = {1}г", productId, quantity));
                }
            }
            Console.ReadKey();
        }
        static public void ReadMenuXML()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("menu.xml");
            int count = 0;
            foreach (XmlNode menuDish in doc.DocumentElement.ChildNodes)
            {
                string id = menuDish["id"].InnerText;
                string dishId = menuDish["dishId"].InnerText;
                string price = menuDish["price"].InnerText;
                string date = menuDish["date"].InnerText;
                Console.WriteLine($"{++count}).");
                Console.WriteLine(string.Format(" Id = {0}\n Id блюда = {1}\n Цена = {2}\n Дата внесения = {3}", id, dishId, price, date));
            }
            Console.ReadKey();
        }
    }
}
