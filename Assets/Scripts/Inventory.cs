using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class Inventory : MonoBehaviour
    {
        public static Inventory Instance { get; private set; }

        private Dictionary<ItemType, int> _inventory = new Dictionary<ItemType, int>();

        private void Awake()
        {
            Instance = this;
            foreach (ItemType type in Enum.GetValues(typeof(ItemType))) _inventory[type] = 0;
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