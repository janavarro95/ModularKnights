using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Items
{
    /// <summary>
    /// Manages a given inventory.
    /// </summary>
    [Serializable,SerializeField]
    public class Inventory
    {
        /// <summary>
        /// All of the items 
        /// </summary>
        public Dictionary<string, Item> items;

        public List<Item> actualItems
        {
            get
            {
                List<Item> localItems = new List<Item>();
                foreach(Item I in items.Values)
                {
                    localItems.Add(I);
                }
                return actualItems;
            }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public Inventory()
        {
            this.items = new Dictionary<string, Item>();
        }

        /// <summary>
        /// Checks if the player's inventory contains a said item.
        /// </summary>
        /// <param name="I"></param>
        /// <returns></returns>
        public bool Contains(Item I)
        {
            return items.ContainsKey(I.Name);
        }

        public bool Contains(string ItemName)
        {
            return items.ContainsKey(ItemName);
        }

        public Item getItem(string ItemName)
        {
            if (Contains(ItemName))
            {
                return items[ItemName];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Alows us to use foreach loops on the items in the inventory.
        /// </summary>
        /// <returns></returns>
        public Dictionary<string,Item>.Enumerator GetEnumerator()
        {
            return items.GetEnumerator();
        }

        /// <summary>
        /// Removes a given item from the player's inventory.
        /// </summary>
        /// <param name="I"></param>
        /// <returns></returns>
        public bool Remove(Item I)
        {
            return this.items.Remove(I.Name);
        }


        public bool containsEnoughOf(Item I,int Amount)
        {
            return containsEnoughOf(I.Name, Amount);
        }

        public bool containsEnoughOf(string ItemName,int Amount)
        {
            if (this.items.ContainsKey(ItemName))
            {
                if (this.items[ItemName].stack >= Amount) return true;
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Removes a specific amount of items from a stack if possible.
        /// </summary>
        /// <param name="I"></param>
        /// <param name="Amount"></param>
        /// <returns></returns>
        public bool removeAmount(Item I,int Amount)
        {
            return removeAmount(I.Name, Amount);
        }

        /// <summary>
        /// Removes a specific amount of items from a stack if possible.
        /// </summary>
        /// <param name="ItemName"></param>
        /// <param name="Amount"></param>
        /// <returns></returns>
        public bool removeAmount(string ItemName,int amount)
        {
            if (containsEnoughOf(ItemName, amount))
            {
                this.items[ItemName].removeFromStack(amount);

                if (this.items[ItemName].stack <= 0)
                {
                    this.items.Remove(ItemName);
                }

                return true;
            }
            return false;
        }

        /// <summary>
        /// Add an item to the player's inventory.
        /// </summary>
        /// <param name="I"></param>
        public void Add(Item I)
        {
            if (!this.items.ContainsKey(I.Name))
            {
                this.items.Add(I.Name, I);
            }
            else
            {
                this.items[I.Name].addToStack(I.stack);
            }
        }

        public void Add(Item I,int amount)
        {
            if (!this.items.ContainsKey(I.Name))
            {
                Item i = (Item)I.clone();
                i.stack = amount;
                this.items.Add(i.Name,i);
            }
            else
            {
                this.items[I.Name].addToStack(amount);
                Debug.Log("STACKY SIZE:"+this.items[I.Name].stack);
            }
        }

        public List<Dish> getAllDishes()
        {
            List<Dish> dishList = new List<Dish>();
            foreach(KeyValuePair<string,Item> pair in this.items)
            {
                if (pair.Value.GetType() == typeof(Dish))
                {
                    dishList.Add((Dish)pair.Value);
                }
            }
            return dishList;
        }
    }
}
