using Core;
using Ui;

namespace Interactions
{
    public class CollectItemInteraction : Interaction
    {
        public ItemType type;
        public int amount;

        public string collectMessage;

        public override void Interact()
        {
            gameObject.SetActive(false);
            InventoryController.AddItem(type, amount);
            ShowDialogue.Show(new Dialogue {duration = 2, message = string.Format(collectMessage, type, amount)});
        }
    }
}