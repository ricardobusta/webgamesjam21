using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class ShowDialogue : MonoBehaviour
{
    public static ShowDialogue Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI label;

    private Tween _dialogueTween;

    private void Awake()
    {
        Instance = this;
        label.text = string.Empty;
    }

    public void Show(params Dialogue[] dialogues)
    {
        _dialogueTween?.Kill();
        var showDialogue = DOTween.Sequence();
        foreach (var dialogue in dialogues)
        {
            showDialogue.AppendCallback(() => label.text = dialogue.message);
            showDialogue.AppendInterval(dialogue.duration);
        }

        showDialogue.AppendCallback(() => label.text = string.Empty);
        _dialogueTween = showDialogue.Play();
    }
}