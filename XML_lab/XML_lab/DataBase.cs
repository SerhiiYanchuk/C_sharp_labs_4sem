using System;
using System.Collections.Generic;


namespace XML_lab
{
    public class Product
    {
        public int id;
        public string name;
        public int calories;
        public Product()
        { }
        public Product(int id, string name, int calories)
        {
            this.id = id;
            this.name = name;
            this.calories = calories;
        }
        public override string ToString()
        {
            return $"id={id}, name={name}, calories={calories}";
        }
    }
    public class Dish
    {
        public int id;
        public string name;
        // id & quantity
        public List<Tuple<int, int>> products;
        public Dish() { }
        public Dish(int id, string name, List<Tuple<int, int>> products)
        {
            this.id = id;
            this.name = name;
            this.products = products;
        }
        public override string ToString()
        {
            return $"id={id}, name={name}";
        }
    }
    public class MenuDish
    {
        public int id;
        public int dishId;
        public float price;
        public DateTime date;
        public MenuDish()
        { }
        public MenuDish(int id, int dishId, float price, DateTime date)
        {
            this.id = id;
            this.dishId = dishId;
            this.price = price;
            this.date = date;
        }
        public override string ToString()
        {
            return $"id={id}, price={price}, date={date}";
        }
    }
}
