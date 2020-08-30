using DefaultNamespace;

public class CollectItemInteraction : Interaction
{
    public ItemType type;
    public int amount;

    public override void Interact()
    {
        gameObject.SetActive(false);
        InventoryController.AddItem(type, amount);
        ShowDialogue.Show(new Dialogue {duration = 2, message = $"Collected {type} x{amount}"});
    }
}