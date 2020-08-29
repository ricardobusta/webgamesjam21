using TMPro;
using UnityEngine;

public class ObjectInspector : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI inspectLabel;
    [SerializeField] private LayerMask interactiveLayer;

    [SerializeField] private float interactionDistance;

    private Collider _inspectedObject;
    private int _interactionLayer;
    private int _outlinedLayer;

    private void Awake()
    {
        _interactionLayer = LayerMask.NameToLayer("Default");
        _outlinedLayer = LayerMask.NameToLayer("Outlined");
    }

    public void PickObject()
    {
        if (_inspectedObject != null)
        {
            
        }
    }
    
    // Update is called once per frame
    private void Update()
    {
        var t = transform;
        if (Physics.Raycast(t.position, t.forward, out var hit, interactionDistance, interactiveLayer))
        {
            var col = hit.collider;
            if (col != _inspectedObject)
            {
                ResetObject();
                if (col.TryGetComponent<InteractiveObject>(out var interaction))
                {
                    _inspectedObject = col;
                    _inspectedObject.gameObject.layer = _outlinedLayer;
                    inspectLabel.text = interaction.Name;
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
        if (_inspectedObject != null)
        {
            _inspectedObject.gameObject.layer = _interactionLayer;
            _inspectedObject = null;
            inspectLabel.text = string.Empty;
        }
    }
}