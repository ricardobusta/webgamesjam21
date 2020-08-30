using System;
using System.Linq;
using Interactions;
using Ui;
using UnityEngine;

namespace Core
{
    public class InteractiveObject : MonoBehaviour
    {
        [SerializeField] private string objName;

        [Serializable]
        public struct InteractionRequirement
        {
            public ItemType type;
            public int amount;
            public bool consumed;
        }

        [SerializeField] private InteractionRequirement[] requirements;

        [SerializeField] private Interaction success;
        [SerializeField] private string failureMessage;

        [SerializeField] private bool interactOnlyOnce;
        
        [SerializeField] private string description;

        private bool _interacted;
        
        public string Name => objName;

        public void Describe()
        {
            ShowDialogue.Show(new Dialogue
            {
                message = description,
                duration = 2
            });
        }
        
        public void Interact()
        {
            if (interactOnlyOnce && _interacted) return;
            
            var missing = (from requirement in requirements
                let has = InventoryController.ItemAmount(requirement.type)
                where requirement.amount > has
                select $"{requirement.type} x{requirement.amount - has}").ToList();

            if (missing.Count == 0)
            {
                foreach (var requirement in requirements)
                {
                    if (requirement.consumed)
                    {
                        InventoryController.AddItem(requirement.type, -requirement.amount);
                    }
                }
                if (success != null) success.Interact();
                _interacted = true;
            }
            else
            {
                ShowDialogue.Show(new Dialogue
                {
                    message = string.Format(failureMessage, string.Join(", ", missing)),
                    duration = 2
                });
            }
        }
    }
}