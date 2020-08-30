using DefaultNamespace;

public class CollectItemInteraction : Interaction
{
    public ItemType type;
    public int amount;

    public override void Interact()
    {
        gameObject.SetActive(false);
        Inventory.Instance.AddItem(type, amount);
        ShowDialogue.Instance.Show(new Dialogue {duration = 1, message = $"Collected {type} x{amount}"});
    }
}