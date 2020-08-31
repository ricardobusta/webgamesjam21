using Core;
using Ui;

namespace Interactions
{
    public class CollectItemInteraction : Interaction
    {
        public ItemType type;
        public int amount;

        public string collectMessage;

        public bool disappear = true;
        private bool _collected = false;

        public override void Interact()
        {
            if (_collected) return;
            if (disappear)
            {
                gameObject.SetActive(false);
            }

            InventoryController.AddItem(type, amount);
            ShowDialogue.ShowNormal(new Dialogue {duration = 2, message = string.Format(collectMessage, type, amount)});
            _collected = true;
        }
    }
}