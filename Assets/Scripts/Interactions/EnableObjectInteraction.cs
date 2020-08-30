using Interactions;
using UnityEngine;

public class EnableObjectInteraction : Interaction
{
    public GameObject[] EnableObjects;
    public GameObject[] DisableObjects;
    
    public override void Interact()
    {
        foreach (var obj in EnableObjects)
        {
            obj.SetActive(true);            
        }

        foreach (var obj in DisableObjects)
        {
            obj.SetActive(false);
        }
    }
}
