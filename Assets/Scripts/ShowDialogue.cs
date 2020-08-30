using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class ShowDialogue : MonoBehaviour
{
    public static ShowDialogue Instance { get; private set; }

    public struct Dialogue
    {
        public float duration;
        public string message;
    }

    [SerializeField] private TextMeshProUGUI label;

    private Tween _dialogueTween;

    private void Awake()
    {
        Instance = this;
        label.text = string.Empty;
    }

    public void Show(IEnumerable<Dialogue> dialogue)
    {
        _dialogueTween?.Kill();
        var showDialogue = DOTween.Sequence();
        foreach (var d in dialogue)
        {
            showDialogue.AppendCallback(() => label.text = d.message);
            showDialogue.AppendInterval(d.duration);
        }

        showDialogue.AppendCallback(() => label.text = string.Empty);
        _dialogueTween = showDialogue.Play();
    }
}
