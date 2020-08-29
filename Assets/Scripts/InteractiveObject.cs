using System;
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    [SerializeField] private string objName;

    [Serializable]
    public class InteractionRequirement
    {
        public string item;
        public int amount;
    }

    [SerializeField] private InteractionRequirement[] requirements;
    
    [SerializeField] private Interaction success;
    [SerializeField] private Interaction fail;

    public string Name => objName;

    public void Interact()
    {
        if (success != null)
        {
            success.Interact();
        }
    }
}
