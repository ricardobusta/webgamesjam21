using System;
using System.Linq;
using DefaultNamespace;
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    [SerializeField] private string objName;

    [Serializable]
    public struct InteractionRequirement
    {
        public ItemType item;
        public int amount;
    }

    [SerializeField] private InteractionRequirement[] requirements;

    [SerializeField] private Interaction success;
    [SerializeField] private string failureMessage;

    public string Name => objName;

    public void Interact()
    {
        var missing = (from requirement in requirements
            let has = InventoryController.ItemAmount(requirement.item)
            where requirement.amount > has
            select $"{requirement.item} x{requirement.amount - has}").ToList();

        if (missing.Count == 0)
        {
            if (success != null) success.Interact();
        }
        else
        {
            ShowDialogue.Show(new Dialogue()
            {
                message = string.Format(failureMessage, string.Join(", ", missing)),
                duration = 2
            });
        }
    }
}