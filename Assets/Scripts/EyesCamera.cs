using System;
using UnityEngine;

public class EyesCamera : MonoBehaviour
{
    public static Transform Transform { get; private set; }

    private void Awake()
    {
        Transform = transform;
    }
}