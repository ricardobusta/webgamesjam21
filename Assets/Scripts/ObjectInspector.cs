using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class ObjectInspector : MonoBehaviour
{
    [SerializeField] private TextMeshPro inspectLabel;
    [SerializeField] private LayerMask interactiveLayer;

    [SerializeField] private float interactionDistance;

    private Interactable _inpsectedObject;
    private bool _hasInspectedObject;

    private RaycastHit[] _hits = new RaycastHit[1];

    // Update is called once per frame
    private void Update()
    {
        var t = transform;
        var ray = new Ray(t.position, t.forward);
        Debug.DrawRay(t.position, t.forward * interactionDistance);
        if (Physics.RaycastNonAlloc(ray, _hits, interactionDistance, interactiveLayer) > 0)
        {
            if (_hits[0].collider.TryGetComponent(out _inpsectedObject))
                {
                    _hasInspectedObject = true;
                    _inpsectedObject.gameObject.layer = LayerMask.NameToLayer("Outline");
                    Debug.Log("Setting Layer");
                }
        }
        else
        {
            if (_hasInspectedObject)
            {
                _inpsectedObject.gameObject.layer = LayerMask.NameToLayer("Default");
                _hasInspectedObject = false;
                Debug.Log("Removing Layer");
            }
        }
    }
}