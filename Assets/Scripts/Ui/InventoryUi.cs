using System.Collections.Generic;
using Core;
using UnityEngine;

namespace Ui
{
    public class InventoryUi : MonoBehaviour
    {
        private static InventoryUi _instance;

        private readonly Dictionary<ItemType, InventoryUiItem> _itemMap = new Dictionary<ItemType, InventoryUiItem>();

        private void Awake()
        {
            _instance = this;
        }

        private void Start()
        {
            foreach (var item in GetComponentsInChildren<InventoryUiItem>())
            {
                _itemMap[item.type] = item;
                UpdateItem(item.type, 0);
            }
        }

        public static void UpdateItem(ItemType type, int amount)
        {
            if (amount == 0)
            {
                _instance._itemMap[type].gameObject.SetActive(false);
            }
            else
            {
                var item = _instance._itemMap[type];
                item.gameObject.SetActive(true);
                item.amount.text = $"x{amount}";
            }
        }
    }
}