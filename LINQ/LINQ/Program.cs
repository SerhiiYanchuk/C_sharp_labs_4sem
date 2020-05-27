using System;
using System.Linq;
using System.Collections.Generic;

namespace LINQ
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Product> products;
            List<Dish> dishes;
            List<MenuDish> menu;
            CreateDB(out products, out dishes, out menu);

            while (true)
            {
                Console.Clear();
                Console.WriteLine("1. Выбрать названия всех продуктов ");
                Console.WriteLine("2. Все продукты, у которых каллорий >=250 на 100г продукта ");
                Console.WriteLine("3. Просортировать блюда в меню по дате внесения и изменения их в меню");
                Console.WriteLine("4. Сгрупировать блюда по продуктам ");
                Console.WriteLine("5. Вывести все блюда и их компоненты");
                Console.WriteLine("6. Выбрать все блюда с меню с актуальными ценами");
                Console.WriteLine("7. Вывести блюда, в которых есть хоть один продукт >=300 кклр на 100г продукта ");
                Console.WriteLine("8. Выбрать названия все блюда, в составе которых есть сахар ");
                Console.WriteLine("9. Выбрать названия всех блюд и количество разных продуктов ");
                Console.WriteLine("10. Выбрать названия всех блюд и калорийность  ");
                Console.WriteLine("11. Выбрать самый калорийный продукт на 100г");
                Console.WriteLine("12. Разница борща от куриного супа");
                Console.WriteLine("13. Общее борща и куриного супа");
                Console.WriteLine("14. Все что нужно для борща и куриного супа");
                Console.WriteLine("15. Меню состоит с 4 страниц по 2 блюдо. Перейти на 3 страницу и вывести блюда");
                int choosen;
                Console.Write("Choose: ");
                choosen = int.Parse(Console.ReadLine());
                Console.Clear();

                switch (choosen)
                {
                    case 1:
                        #region 1. Выбрать названия всех продуктов  
                        var q1 = from r in products
                                 select r.name;

                        // var q1 = products.Select(r => r.name);

                        foreach (var temp in q1)
                        {
                            Console.WriteLine("Продукт: " + temp);
                        }
                        #endregion
                        Console.ReadKey();
                        break;

                    case 2:

                        #region 2. Все продукты, у которых каллорий >=250 на 100г продукта 
                        var q2 = from r in products
                                 where r.calories >= 250
                                 select $"{r.name} - {r.calories}";

                        //var q2 = products.Where(r => r.calories >= 250).Select(r => $"{r.name} - {r.calories}");\

                        foreach (var temp in q2)
                        {
                            Console.WriteLine("Продукты " + temp);
                        }
                        #endregion
                        Console.ReadKey();
                        break;

                    case 3:

                        #region 3. Просортировать блюда в меню по дате внесения и изменения их в меню
                        var q3 = from r in menu
                                 orderby r.date, r.dishId
                                 select $"{r.date} - {r.dishId}";

                        //var q3 = menu.OrderBy(r => r.date).ThenBy(r => r.dishId).Select(r => $"{r.date} - {r.dishId}");

                        foreach (var temp in q3)
                        {
                            Console.WriteLine(temp);
                        }
                        #endregion
                        Console.ReadKey();
                        break;

                    case 4:
                        #region 4. Сгрупировать блюда по продуктам 
                        var q4 = from r in dishes
                                 from p in r.products
                                 group r by p.Item1;

                        foreach (var group in q4)
                        {
                            Console.WriteLine("Id продукта: " + group.Key);
                            foreach (var temp in group)
                                Console.WriteLine(" - " + temp.name);
                            Console.WriteLine();
                        }

                        //var q4 = dishes.SelectMany(r => r.products,
                        //               (r, p) => new { Dish = r, Product = p })
                        //               .GroupBy(u => u.Product.Item1);

                        //foreach (var group in q4)
                        //{
                        //    Console.WriteLine(group.Key);
                        //    foreach (var temp in group)
                        //        Console.WriteLine(temp.Dish.name);
                        //    Console.WriteLine();
                        //}
                        #endregion
                        Console.ReadKey();
                        break;

                    case 5:
                        #region 5. Вывести все блюда и их компоненты
                        var q5 = from r in dishes
                                 from p1 in r.products
                                 join p2 in products on p1.Item1 equals p2.id
                                 group p2.name by r.name;

                        foreach (var group in q5)
                        {
                            Console.WriteLine($"Блюдо: {group.Key}\nПродукты:");
                            foreach (var temp in group)
                                Console.WriteLine($" - {temp}");
                            Console.WriteLine();
                        }

                        //var q5 = dishes.SelectMany(r => r.products,
                        //                (r, p) => new { dishName = r.name, product = p })
                        //                .Join(products,
                        //                   p1 => p1.product.Item1,
                        //                   p2 => p2.id,
                        //                   (p1, p2) => new
                        //                   {
                        //                       dishName = p1.dishName,
                        //                       productName = p2.name
                        //                   })
                        //                .GroupBy(r => r.dishName);

                        //foreach (var group in q5)
                        //{
                        //    Console.WriteLine($"Блюдо: {group.Key}\nПродукты:");
                        //    foreach (var temp in group)
                        //        Console.WriteLine($" - {temp.productName}");
                        //    Console.WriteLine();
                        //}
                        #endregion
                        Console.ReadKey();
                        break;

                    case 6:
                        #region 6. Выбрать все блюда с меню с актуальными ценами
                        //var q6 = menu.OrderByDescending(r => r.date).Distinct(new MenuDishComparer());

                        //foreach (var temp in q6)
                        //{
                        //    Console.WriteLine($"Id блюда = {temp.dishId}, цена = {temp.price}, дата внесения/изменения = {temp.date}");
                        //}

                        var q6 = menu.OrderByDescending(r => r.date).Distinct(new MenuDishComparer())
                                    .Join(dishes, r1 => r1.dishId, r2 => r2.id, (r1, r2) => new
                                    {
                                        dishName = r2.name,
                                        price = r1.price,
                                        date = r1.date
                                    });

                        foreach (var temp in q6)
                        {
                            Console.WriteLine($"Названия блюда = {temp.dishName}, цена = {temp.price}, дата внесения/изменения = {temp.date}");
                        }

                        #endregion
                        Console.ReadKey();
                        Console.ReadKey();
                        break;

                    case 7:
                        #region 7. Вывести блюда, в которых есть хоть один продукт >=300 кклр на 100г продукта 
                        var q7 = from r in dishes
                                 where r.products.Join(products, p1 => p1.Item1, p2 => p2.id, (p1, p2) => p2.calories).Any(p => p >= 300)
                                 select r.name;

                        //var q7 = from r in dishes
                        //         where (from p1 in r.products
                        //                join p2 in products on p1.Item1 equals p2.id
                        //                select p2.calories).Any(p => p >= 300)
                        //         select r.name;

                        foreach (var temp in q7)
                        {
                            Console.WriteLine(temp);
                        }
                        #endregion
                        Console.ReadKey();
                        break;

                    case 8:
                        #region 8. Выбрать названия все блюда, в составе которых есть сахар 
                        var q8 = from r in dishes
                                 where r.products.Join(products, p1 => p1.Item1, p2 => p2.id, (p1, p2) => p2.name).Contains("сахар")
                                 select r.name;

                        //var q8 = from r in dishes
                        //         where (from p in r.products select p.Item1).Contains(3)
                        //         select r.name;


                        foreach (var temp in q8)
                        {
                            Console.WriteLine(temp);
                        }
                        #endregion
                        Console.ReadKey();
                        break;

                    case 9:
                        #region 9. Выбрать названия всех блюд и количество разных продуктов 
                        var q9 = from r in dishes
                                 select new
                                 {
                                     dishName = r.name,
                                     quantityProducts = r.products.Count(),
                                 };

                        foreach (var temp in q9)
                        {
                            Console.WriteLine($"Название блюда = {temp.dishName}, к-ство разных продуктов = {temp.quantityProducts}");
                        }
                        #endregion
                        Console.ReadKey();
                        break;

                    case 10:
                        #region 10. Выбрать названия всех блюд и калорийность  
                        var q10 = from r in dishes
                                  select new
                                  {
                                      dishName = r.name,
                                      calories =
                                                 (from p1 in r.products
                                                  join p2 in products on p1.Item1 equals p2.id
                                                  select (float)p1.Item2 * p2.calories / 100
                                                 )
                                                  .Aggregate((p1, p2) => p1 + p2)
                                  };

                        foreach (var temp in q10)
                        {
                            Console.WriteLine($"Название блюда = {temp.dishName}, калории  = {temp.calories}");
                        }
                        #endregion
                        Console.ReadKey();
                        break;

                    case 11:
                        #region 11. Выбрать самый калорийный продукт на 100г
                        int cl = products.Max(p => p.calories);

                        var q110 = products.Where(p => p.calories == cl).Select(p => new { name = p.name, cl = p.calories });

                        var q111 = from r in products
                                  let clMax = products.Max(p => p.calories)
                                  where r.calories == clMax
                                  select new
                                  {
                                      name = r.name,
                                      cl = r.calories
                                  };
                        foreach (var temp in q110)
                        {
                            Console.WriteLine($"Название самого калорийного продукта = {temp.name}, калории  = {temp.cl}");
                        }

                        foreach (var temp in q111)
                        {
                            Console.WriteLine($"Название самого калорийного продукта = {temp.name}, калории  = {temp.cl}");
                        }

                        #endregion
                        Console.ReadKey();
                        break;

                    case 12:
                        #region 12. Разница борща от куриного супа
                        var q12 = dishes.ElementAt(2).products.Except(dishes.ElementAt(3).products)
                                        .Join(products, p1 => p1.Item1, p2 => p2.id, (p1, p2) => p2.name);

                        foreach (var temp in q12)
                        {
                            Console.WriteLine(temp);
                        }
                        #endregion
                        Console.ReadKey();
                        break;

                    case 13:
                        #region 13. Общее борща и куриного супа
                        var q13 = dishes.ElementAt(2).products.Intersect(dishes.ElementAt(3).products)
                                        .Join(products, p1 => p1.Item1, p2 => p2.id, (p1, p2) => p2.name);

                        foreach (var temp in q13)
                        {
                            Console.WriteLine(temp);
                        }
                        #endregion
                        Console.ReadKey();
                        break;

                    case 14:
                        #region 14. Все что нужно для борща и куриного супа
                        var q14 = dishes.ElementAt(2).products.Union(dishes.ElementAt(3).products)
                                        .Join(products, p1 => p1.Item1, p2 => p2.id, (p1, p2) => p2.name);

                        foreach (var temp in q14)
                        {
                            Console.WriteLine(temp);
                        }
                        #endregion
                        Console.ReadKey();
                        break;

                    case 15:
                        #region 15. Меню состоит с 4 страниц по 2 блюдо. Перейти на 3 страницу и вывести блюда
                        int page = 3;
                        int dishesOnPage = 2;
                        var correctMenu = menu.OrderByDescending(r => r.date).Distinct(new MenuDishComparer()).OrderBy(r => r.dishId);
                        var q15 = from r in correctMenu.Skip((page - 1) * dishesOnPage).Take(dishesOnPage)
                                 join d in dishes on r.dishId equals d.id
                                 select new
                                 {
                                     name = d.name,
                                     price = r.price,
                                     prod = from p1 in d.products
                                            join p2 in products on p1.Item1 equals p2.id
                                            select p2.name
                                 };

                        foreach (var temp in q15)
                        {
                            Console.WriteLine($"Названия блюда - {temp.name}, цена = {temp.price}");
                            foreach (var p in temp.prod)
                                Console.WriteLine($" - {p}");
                        }
                        #endregion
                        Console.ReadKey();
                        break;
                    case 0:
                        return;
                    default:
                        break;
                }
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
                new MenuDish(9, 2, 300, new DateTime(2020, 4, 12)),
                new MenuDish(10, 3, 350, new DateTime(2020, 4, 9)),
                new MenuDish(11, 3, 325, new DateTime(2020, 6, 13)),
                new MenuDish(12, 6, 240, new DateTime(2020, 7, 16)),
                new MenuDish(13, 7, 130, new DateTime(2020, 7, 23))
            };
        }
    }
}
