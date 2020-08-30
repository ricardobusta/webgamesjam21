using System;
using System.Linq;
using Interactions;
using Ui;
using UnityEngine;

namespace Core
{
    public class InteractiveObject : InteractiveEntity
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

        [SerializeField] private Interaction[] successInteractions;
        [SerializeField] private string failureMessage;

        [SerializeField] private bool interactOnlyOnce;

        [SerializeField] private string description;

        private bool _interacted;

        public override string Name => objName;
        public override InteractiveObject MainObject => this;
        private Collider _collider;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
            var proxies = GetComponentsInChildren<ProxyInteractiveObject>();
            foreach (var proxy in proxies)
            {
                proxy.mainObject = this;
            }
        }

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

                foreach (var successInteraction in successInteractions)
                {
                    if (successInteraction != null) successInteraction.Interact();

                }
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