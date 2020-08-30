using System;
using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUiItem : MonoBehaviour
{
    public ItemType type;
    public Image icon;
    public TextMeshProUGUI amount;
}

public class InventoryUi : MonoBehaviour
{
    private static InventoryUi _instance;

    public InventoryUiItem[] items;

    private readonly Dictionary<ItemType, InventoryUiItem> _itemMap = new Dictionary<ItemType, InventoryUiItem>();

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        foreach (var item in items)
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