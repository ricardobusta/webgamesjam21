﻿using Core;
using TMPro;
using UnityEngine;

namespace Player
{
    public class ObjectInspector : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI inspectLabel;
        [SerializeField] private LayerMask interactiveLayer;

        [SerializeField] private float interactionDistance;

        private Collider _inspectedCollider;
        private InteractiveObject _inspectedObject;
        private bool _hasInspectedObject;
        private int _interactionLayer;
        private int _outlinedLayer;

        private void Awake()
        {
            _interactionLayer = LayerMask.NameToLayer("Default");
            _outlinedLayer = LayerMask.NameToLayer("Outlined");
            inspectLabel.text = string.Empty;
        }

        public void PickObject()
        {
            if (_hasInspectedObject) _inspectedObject.Interact();
        }

        public void InspectObject()
        {
            if (_hasInspectedObject) _inspectedObject.Describe();
        }

        // Update is called once per frame
        private void Update()
        {
            var t = transform;
            if (Physics.Raycast(t.position, t.forward, out var hit, interactionDistance, interactiveLayer))
            {
                var col = hit.collider;
                if (col != _inspectedCollider)
                {
                    ResetObject();
                    if (col.TryGetComponent<InteractiveEntity>(out var interaction))
                    {
                        _inspectedCollider = col;
                        _inspectedObject = interaction.MainObject;
                        _inspectedObject.gameObject.layer = _outlinedLayer;
                        foreach (Transform child in _inspectedObject.transform)
                        {
                            child.gameObject.layer = _outlinedLayer;
                        }
                        inspectLabel.text = _inspectedObject.Name;
                        _hasInspectedObject = true;
                    }
                }
            }
            else
            {
                ResetObject();
            }
        }

        private void ResetObject()
        {
            if (_hasInspectedObject)
            {
                _inspectedObject.gameObject.layer = _interactionLayer;
                foreach (Transform child in _inspectedObject.transform)
                {
                    child.gameObject.layer = _interactionLayer;
                }
                _inspectedCollider = null;
                _inspectedObject = null;
                inspectLabel.text = string.Empty;
                _hasInspectedObject = false;
            }
        }
    }
}