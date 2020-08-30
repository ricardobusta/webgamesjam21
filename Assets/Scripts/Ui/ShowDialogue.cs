using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Ui
{
    public class ShowDialogue : MonoBehaviour
    {
        private static ShowDialogue _instance;

        [SerializeField] private TextMeshProUGUI label;

        private Tween _dialogueTween;

        private void Awake()
        {
            _instance = this;
            label.text = string.Empty;
        }

        public static void Show(params Dialogue[] dialogues)
        {
            _instance._dialogueTween?.Kill();
            var showDialogue = DOTween.Sequence();
            foreach (var dialogue in dialogues)
            {
                showDialogue.AppendCallback(() => _instance.label.text = dialogue.message);
                showDialogue.AppendInterval(dialogue.duration);
            }

            showDialogue.AppendCallback(() => _instance.label.text = string.Empty);
            _instance._dialogueTween = showDialogue.Play();
        }
    }
}