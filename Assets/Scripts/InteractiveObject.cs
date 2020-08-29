using System;
using UnityEngine;
using UnityEngine.Events;

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
    
    [SerializeField] private UnityEvent onInteractSuccess;
    [SerializeField] private UnityEvent onInteractFail;

    public string Name => objName;

    public void Interact()
    {
        onInteractSuccess?.Invoke();
    }
}
