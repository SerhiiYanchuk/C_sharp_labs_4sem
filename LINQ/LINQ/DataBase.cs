using System;
using System.Collections.Generic;

namespace LINQ
{
    class Product
    {
        public int id; 
        public string name;
        public int calories;
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
    class Dish
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
    class MenuDish
    {
        public int id; 
        public int dishId;
        public float price;
        public DateTime date;
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
