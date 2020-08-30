using Core;
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
        private int _outlinedLayer;
        private int _nonCollideLayer;
        private int _outlinedNonCollideLayer;

        private void Awake()
        {
            _nonCollideLayer = LayerMask.NameToLayer("NonCollide");
            _outlinedLayer = LayerMask.NameToLayer("Outlined");
            _outlinedNonCollideLayer = LayerMask.NameToLayer("NonCollideOutlined");
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
                        var go = _inspectedObject.gameObject;
                        var outlineLayer = go.layer == _nonCollideLayer
                            ? _outlinedNonCollideLayer
                            : _outlinedLayer;
                        go.layer = outlineLayer;
                        foreach (Transform child in _inspectedObject.transform) child.gameObject.layer = outlineLayer;
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
                _inspectedObject.gameObject.layer = _inspectedObject.DefaultLayer;
                foreach (Transform child in _inspectedObject.transform)
                    child.gameObject.layer = _inspectedObject.DefaultLayer;
                _inspectedCollider = null;
                _inspectedObject = null;
                inspectLabel.text = string.Empty;
                _hasInspectedObject = false;
            }
        }
    }
}