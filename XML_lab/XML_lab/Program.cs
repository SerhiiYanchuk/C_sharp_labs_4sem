using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using System.Linq;

namespace XML_lab
{
    partial class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("1. Создать XML файлы");
                Console.WriteLine("2. Прочесть products.xml");
                Console.WriteLine("3. Прочесть dishes.xml");
                Console.WriteLine("4. Прочесть menu.xml");
                Console.WriteLine("5. Прочесть products.xml с помощью LINQ");
                Console.WriteLine("6. Выбрать названия всех продуктов");
                Console.WriteLine("7. Все продукты, у которых каллорий >=250 на 100г продукта ");
                Console.WriteLine("8. Просортировать блюда в меню по дате внесения и изменения их в меню");
                Console.WriteLine("9. Сгрупировать блюда по продуктам");
                Console.WriteLine("10. Вывести все блюда и их компоненты");
                Console.WriteLine("11. Вывести блюда, в которых есть хоть один продукт >=300 кклр на 100г продукта ");
                Console.WriteLine("12. Выбрать названия всех блюд и калорийность ");
                Console.WriteLine("13. Выбрать самый калорийный продукт на 100г");
                Console.WriteLine("14. Добавить блюдо в XML");
                Console.WriteLine("15. Удаления блюда из меню");
                Console.WriteLine("0. Exit");
                int choosen;
                Console.Write("Choose: ");
                choosen = int.Parse(Console.ReadLine());
                Console.Clear();

                switch (choosen)
                {
                    case 1:
                        CreateXMLfiles();
                        break;
                    case 2:
                        ReadProductsXML();
                        break;
                    case 3:
                        ReadDishesXML();
                        break;
                    case 4:
                        ReadMenuXML();
                        break;
                    case 5:
                        #region
                        XElement root = XElement.Load("products.xml");
                        int count = 0;
                        foreach(var product in root.Elements())
                        {
                            // string id = (string)product.Element("id");
                            string id = product.Element("id").Value;
                            string name = product.Element("name").Value;
                            string calorise = product.Element("calories").Value;
                            Console.WriteLine($"{++count}).");
                            Console.WriteLine(string.Format(" Id = {0}\n продукт = {1}\n каллорий на 100г = {2}", id, name, calorise));
                        }
                        Console.ReadKey();
                        #endregion
                        break;
                    case 6:
                        #region 1. Выбрать названия всех продуктов
                        var q1 = from r in XElement.Load("products.xml").Elements("product")
                                 select r.Element("name").Value;

                        Console.WriteLine($"Названия продуктов:");
                        foreach (var p in q1)
                        {
                            Console.WriteLine($" - {p}");
                        }
                        Console.ReadKey();
                        #endregion
                        break;
                    case 7:
                        #region 2. Все продукты, у которых каллорий >=250 на 100г продукта 
                        var q2 = from r in XElement.Load("products.xml").Elements("product")
                                 where int.Parse(r.Element("calories").Value) >= 250
                                 select new
                                 {
                                     name = r.Element("name").Value,
                                     calories = r.Element("calories").Value
                                 };
                                
                        foreach (var p in q2)
                        {
                            Console.WriteLine($"Название продукта: {p.name}, каллории: {p.calories}");
                        }
                        Console.ReadKey();
                        #endregion
                        break;
                    case 8:
                        #region 3. Просортировать блюда в меню по дате внесения и изменения их в меню
                        var q3 = from r in XElement.Load("menu.xml").Elements("menuDish")
                                 let date = DateTime.ParseExact(r.Element("date").Value, "dd.MM.yyyy HH:mm:ss", null)
                                 let dishId = int.Parse(r.Element("dishId").Value)
                                 orderby date, dishId
                                 select $"{date} - {dishId}";

                        foreach (var p in q3)
                        {
                            Console.WriteLine(p);
                        }
                        Console.ReadKey();
                        #endregion
                        break;
                    case 9:
                        #region 4. Сгрупировать блюда по продуктам 
                        var q4 = from r in XElement.Load("dishes.xml").Elements("dish")
                                 from p in r.Element("products").Elements("product")
                                 group r.Element("name").Value by p.Element("productId").Value;

                        foreach (var p in q4)
                        {
                            Console.WriteLine("Id продукта: " + p.Key);
                            foreach (var temp in p)
                                Console.WriteLine(" - " + temp);
                            Console.WriteLine();
                        }
                        Console.ReadKey();
                        #endregion
                        break;
                    case 10:
                        #region 5. Вывести все блюда и их компоненты
                        var q5 = from r in XElement.Load("dishes.xml").Elements("dish")
                                 from p1 in r.Element("products").Elements("product")
                                 join p2 in XElement.Load("products.xml").Elements("product")
                                         on p1.Element("productId").Value equals p2.Element("id").Value
                                 group p2.Element("name").Value by r.Element("name").Value;

                        foreach (var p in q5)
                        {
                            Console.WriteLine($"Блюдо: {p.Key}\nПродукты:");
                            foreach (var temp in p)
                                Console.WriteLine($" - {temp}");
                            Console.WriteLine();
                        }
                        Console.ReadKey();
                        #endregion
                        break;
                    case 11:
                        #region 6. Вывести блюда, в которых есть хоть один продукт >=300 кклр на 100г продукта 
                        var q6 = from r in XElement.Load("dishes.xml").Elements("dish")
                                 where (from p1 in r.Element("products").Elements("product")
                                        join p2 in XElement.Load("products.xml").Elements("product")
                                        on p1.Element("productId").Value equals p2.Element("id").Value
                                        select int.Parse(p2.Element("calories").Value))
                                        .Any(p => p >= 300)
                                 select r.Element("name").Value;

                        Console.WriteLine($"Названия блюд:");
                        foreach (var p in q6)
                        {
                            Console.WriteLine($" - {p}");
                        }
                        Console.ReadKey();
                        #endregion
                        break;
                    case 12:
                        #region 7. Выбрать названия всех блюд и калорийность  
                        var q7 = from r in XElement.Load("dishes.xml").Elements("dish")
                                 select new
                                 {
                                     dishName = r.Element("name").Value,
                                     calories =
                                                (from p1 in r.Element("products").Elements("product")
                                                 join p2 in XElement.Load("products.xml").Elements("product")
                                                 on p1.Element("productId").Value equals p2.Element("id").Value
                                                 select float.Parse(p1.Element("quantity").Value) * int.Parse(p2.Element("calories").Value) / 100)
                                                 .Aggregate((p1, p2) => p1 + p2)                   
                                 };

                        foreach (var temp in q7)
                        {
                            Console.WriteLine($"Название блюда = {temp.dishName}, калории  = {temp.calories}");
                        }
                        Console.ReadKey();
                        #endregion
                        break;
                    case 13:
                        #region 8. Выбрать самый калорийный продукт на 100г
                        var products = XElement.Load("products.xml").Elements("product");
                        var q8 = from r in products
                                 let clMax = products.Max(p => int.Parse(p.Element("calories").Value))
                                 where int.Parse(r.Element("calories").Value) == clMax
                                 select new
                                 {
                                     name = r.Element("name").Value,
                                     cl = int.Parse(r.Element("calories").Value)
                                 };


                        foreach (var temp in q8)
                        {
                            Console.WriteLine($"Название самого калорийного продукта = {temp.name}, калории  = {temp.cl}");
                        }
                        Console.ReadKey();
                        #endregion
                        break;
                    case 14:
                        #region 9. Добавить блюдо в XML
                        Console.WriteLine("Форма заполнения нового блюда");
                        Console.Write("Id блюда: ");
                        string newId = Console.ReadLine();

                        Console.Write("Название блюда: ");
                        string newName = Console.ReadLine();
                        Console.WriteLine("Продукты:");

                        List<Tuple<int, int>> pr = new List<Tuple<int, int>>();
                        while (true)
                        {
                            Console.Write("Id продукта: ");
                            int productId = int.Parse(Console.ReadLine());

                            Console.Write("Количества продукта(г): ");
                            int quantity = int.Parse(Console.ReadLine());

                            pr.Add(Tuple.Create(productId, quantity));
                            Console.Write("Введите 1, чтобы продолжить: ");
                            if (Console.ReadLine() != "1") break;
                        }

                        XElement newEl = new XElement("dish", 
                                                        new XElement("id", newId), 
                                                        new XElement("name", newName),
                                                        new XElement("products"));
                        foreach (var p in pr)
                        {
                            XElement nodePr = new XElement("product",
                                                        new XElement("productId", p.Item1),
                                                        new XElement("quantity", p.Item2));
                            newEl.Element("products").Add(nodePr);
                        }
                        XElement dishRoot = XElement.Load("dishes.xml");
                        dishRoot.Add(newEl);
                        dishRoot.Save("updateDishes.xml");
                        Console.WriteLine("Добавлено");
                        Console.ReadKey();
                        #endregion
                        break;
                    case 15:
                        #region 10. Удаления блюда из меню
                        Console.Write("Id блюда в меню, которое нужно удалить: ");
                        string deleteName = Console.ReadLine();
                        XElement tempRoot = XElement.Load("menu.xml");
                        //var deleteDish = (from r in tempRoot.Elements("menuDish")
                        //                  where r.Element("name").Value == deleteName
                        //                  select r).FirstOrDefault();
                        //deleteDish?.Remove();
                        tempRoot.Elements("menuDish").Where(r => r.Element("id").Value == deleteName).FirstOrDefault().Remove();                        
                        tempRoot.Save("updateMenu.xml");
                        Console.WriteLine("Удалено");
                        Console.ReadKey();
                        #endregion
                        break;
                    case 0:
                        return;
                    default:
                        break;
                }
            }
        }
        
        static public void CreateXMLfiles()
        {
            List<Product> products;
            List<Dish> dishes;
            List<MenuDish> menu;
            CreateDB(out products, out dishes, out menu);

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;

            using (XmlWriter writer = XmlWriter.Create("products.xml", settings))
            {
                writer.WriteStartElement("products");

                foreach (var product in products)
                {
                    writer.WriteStartElement("product");
                    writer.WriteElementString("id", product.id.ToString());
                    writer.WriteElementString("name", product.name);
                    writer.WriteElementString("calories", product.calories.ToString());
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
            }

            using (XmlWriter writer = XmlWriter.Create("dishes.xml", settings))
            {
                writer.WriteStartElement("dishes");

                foreach (var dish in dishes)
                {
                    writer.WriteStartElement("dish");
                    writer.WriteElementString("id", dish.id.ToString());
                    writer.WriteElementString("name", dish.name);
                    writer.WriteStartElement("products");
                    foreach (var product in dish.products)
                    {
                        writer.WriteStartElement("product");
                        writer.WriteElementString("productId", product.Item1.ToString());
                        writer.WriteElementString("quantity", product.Item2.ToString());
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
            }

            using (XmlWriter writer = XmlWriter.Create("menu.xml", settings))
            {
                writer.WriteStartElement("menu");

                foreach (var menuDish in menu)
                {
                    writer.WriteStartElement("menuDish");
                    writer.WriteElementString("id", menuDish.id.ToString());
                    writer.WriteElementString("dishId", menuDish.dishId.ToString());
                    writer.WriteElementString("price", menuDish.price.ToString());
                    writer.WriteElementString("date", menuDish.date.ToString());
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
            }
        }
        static public void CreateDB(out List<Product> products, out List<Dish> dishes, out List<MenuDish> menu)
        {
            products = new List<Product>()
            {
                new Product(1, "рисовая крупа", 330),
                new Product(2, "чернослив", 240),
                new Product(3, "сахар", 387),
                new Product(4, "масло сливочное", 717),
                new Product(5, "тыква", 26),
                new Product(6, "маргарин", 717),
                new Product(7, "манная крупа", 360),
                new Product(9, "свекла", 43),
                new Product(10, "капуста", 25),
                new Product(11, "морковь", 32),
                new Product(12, "петрушка ", 36),
                new Product(13, "лук репчатый", 40),
                new Product(14, "томатная паста", 82),
                new Product(15, "бульон мясной", 6),
                new Product(16, "курица", 170),
                new Product(17, "макаронные изделия", 131),
                new Product(18, "соль", 0),
                new Product(19, "говядина", 250),
                new Product(20, "шпик свиной", 871),
                new Product(21, "свинина", 146),
                new Product(22, "картофель", 77),
                new Product(23, "черемша", 35),
                new Product(24, "мука", 364),
                new Product(25, "яйцо куриное", 155),
                new Product(26, "молоко коровье", 62),
                new Product(27, "ванилин", 288)
            };

            dishes = new List<Dish>()
            {
                new Dish(1,"Рисовая каша", new List<Tuple<int, int>>()
                {
                    Tuple.Create(1, 300),
                    Tuple.Create(2, 160),
                    Tuple.Create(3, 40),
                    Tuple.Create(4, 40)
                }),
                new Dish(2,"Каша из тыквы с манкой", new List<Tuple<int, int>>()
                {
                    Tuple.Create(5, 195),
                    Tuple.Create(6, 20),
                    Tuple.Create(7, 24),
                    Tuple.Create(3, 10)
                }),
                new Dish(3,"Борщ", new List<Tuple<int, int>>()
                {
                    Tuple.Create(9, 160),
                    Tuple.Create(10, 120),
                    Tuple.Create(11, 40),
                    Tuple.Create(12, 10),
                    Tuple.Create(13, 40),
                    Tuple.Create(14, 30),
                    Tuple.Create(3, 10),
                    Tuple.Create(15, 800)
                }),
                new Dish(4,"Куриный суп", new List<Tuple<int, int>>()
                {
                    Tuple.Create(16, 1000),
                    Tuple.Create(11, 40),
                    Tuple.Create(13, 40),
                    Tuple.Create(12, 10),
                    Tuple.Create(17, 150),
                    Tuple.Create(18, 10)
                }),
                new Dish(5,"Бифштекс рубленый", new List<Tuple<int, int>>()
                {
                    Tuple.Create(19, 155),
                    Tuple.Create(20, 18),
                    Tuple.Create(18, 10)
                }),
                new Dish(6,"Жаркое из свинины", new List<Tuple<int, int>>()
                {
                    Tuple.Create(21, 2000),
                    Tuple.Create(13, 80),
                    Tuple.Create(11, 80),
                    Tuple.Create(18, 10)
                }),
                new Dish(7,"Картофель в фольге", new List<Tuple<int, int>>()
                {
                    Tuple.Create(22, 600),
                    Tuple.Create(23, 40),
                    Tuple.Create(18, 10),
                    Tuple.Create(4, 90)
                }),
                new Dish(8,"Вафли", new List<Tuple<int, int>>()
                {
                    Tuple.Create(24, 130),
                    Tuple.Create(4, 20),
                    Tuple.Create(25, 65),
                    Tuple.Create(3, 20),
                    Tuple.Create(26, 200),
                    Tuple.Create(27, 2)
                })
            };

            menu = new List<MenuDish>()
            {
                new MenuDish(1, 1, 100, new DateTime(2020, 1, 1)),
                new MenuDish(2, 2, 200, new DateTime(2020, 1, 22)),
                new MenuDish(3, 3, 250, new DateTime(2020, 2, 10)),
                new MenuDish(4, 4, 150, new DateTime(2020, 2, 3)),
                new MenuDish(5, 5, 300, new DateTime(2020, 3, 1)),
                new MenuDish(6, 6, 200, new DateTime(2020, 1, 1)),
                new MenuDish(7, 7, 100, new DateTime(2020, 1, 1)),
                new MenuDish(8, 8, 500, new DateTime(2020, 1, 5)),
                //new MenuDish(9, 2, 300, new DateTime(2020, 4, 12)),
                //new MenuDish(10, 3, 350, new DateTime(2020, 4, 9)),
                //new MenuDish(11, 3, 325, new DateTime(2020, 6, 13)),
                //new MenuDish(12, 6, 240, new DateTime(2020, 7, 16)),
                //new MenuDish(13, 7, 130, new DateTime(2020, 7, 23))
            };
        }
    }
}
