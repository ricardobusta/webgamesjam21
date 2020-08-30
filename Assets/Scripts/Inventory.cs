using System;
using System.Collections.Generic;

namespace DefaultNamespace
{
    public class Inventory
    {
        public static Inventory Instance { get; private set; }

        public enum ItemType {
            Key
        }

        private Dictionary<ItemType, int> _inventory;
        
        private void Awake()
        {
            Instance = this;
            foreach (ItemType type in Enum.GetValues(typeof(ItemType)))
            {
                _inventory[type] = 0;
            }
        }

        public void AddItem(ItemType type, int amount)
        {
            _inventory[type] += amount;
        }

        public int ItemAmount(ItemType type)
        {
            return _inventory[type];
        }
    }
}