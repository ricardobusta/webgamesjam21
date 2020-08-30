using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class InventoryController : MonoBehaviour
    {
        private static InventoryController _instance;

        private readonly Dictionary<ItemType, int> _inventory = new Dictionary<ItemType, int>();

        private void Awake()
        {
            _instance = this;
            foreach (ItemType type in Enum.GetValues(typeof(ItemType))) _inventory[type] = 0;
        }

        public static void AddItem(ItemType type, int amount)
        {
            _instance._inventory[type] += amount;
        }

        public static int ItemAmount(ItemType type)
        {
            return _instance._inventory[type];
        }
    }
}