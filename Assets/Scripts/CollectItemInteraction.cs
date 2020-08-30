using DefaultNamespace;

public class CollectItemInteraction : Interaction
{
    public Inventory.ItemType type;
    public int amount;
    
    public override void Interact()
    {
        gameObject.SetActive(false);
    }
}
