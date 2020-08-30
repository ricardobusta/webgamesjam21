using Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
    public class InventoryUiItem : MonoBehaviour
    {
        public ItemType type;
        public Image icon;
        public TextMeshProUGUI amount;
    }
}