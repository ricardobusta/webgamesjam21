using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectInspector : MonoBehaviour
{
    [SerializeField] private TextMeshPro _inspectLabel;
    
    private Interactable _inpsectedObject;
    private bool _hasInspectedObject;

    // Update is called once per frame
    void Update()
    {
        
    }
}
