using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Ui
{
    public class ShowDialogue : MonoBehaviour
    {
        private static ShowDialogue _instance;

        [SerializeField] private TextMeshProUGUI label;
        [SerializeField] private TextMeshProUGUI tutorialLabel;

        private Tween _dialogueTween;

        private void Awake()
        {
            _instance = this;
            label.text = string.Empty;
        }

        public static void ShowTutorial(params Dialogue[] dialogues)
        {
            Show(_instance.tutorialLabel, dialogues);
        }
        
        public static void ShowNormal(params Dialogue[] dialogues)
        {
            Show(_instance.label, dialogues);
        }

        private static void Show(TextMeshProUGUI label, params Dialogue[] dialogues)
        {
            _instance._dialogueTween?.Kill();
            var showDialogue = DOTween.Sequence();
            foreach (var dialogue in dialogues)
            {
                showDialogue.AppendCallback(() => label.text = dialogue.message);
                showDialogue.AppendInterval(dialogue.duration);
            }

            showDialogue.AppendCallback(() => label.text = string.Empty);
            _instance._dialogueTween = showDialogue.Play();            
        }
    }
}