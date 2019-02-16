using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Items
{
    public class Dish:Item
    {
        public List<Item> ingredients;

        public Dish() : base()
        {
            ingredients = new List<Item>();
        }

        public Dish(string DishName): base(DishName)
        {
            ingredients = new List<Item>();
        }

        public Dish(string DishName, List<Item> Ingredients):base(DishName)
        {
            this.ingredients = Ingredients;
        }

        public override Item clone()
        {
            return new Dish(this.Name, this.ingredients);
        }
    }
}
