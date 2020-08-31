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
        private Tween _tutorialTween;

        private void Awake()
        {
            _instance = this;
            label.text = string.Empty;
        }

        public static void ShowTutorial(params Dialogue[] dialogues)
        {
            _instance._tutorialTween?.Kill();
            _instance._tutorialTween = Show(_instance.tutorialLabel, dialogues);
            _instance._tutorialTween.Play();
        }
        
        public static void ShowNormal(params Dialogue[] dialogues)
        {
            _instance._dialogueTween?.Kill();
            _instance._dialogueTween = Show(_instance.label, dialogues);
            _instance._dialogueTween.Play();
        }

        private static Sequence Show(TMP_Text label, params Dialogue[] dialogues)
        {
            var showDialogue = DOTween.Sequence();
            foreach (var dialogue in dialogues)
            {
                showDialogue.AppendCallback(() => label.text = dialogue.message);
                showDialogue.AppendInterval(dialogue.duration);
            }

            showDialogue.AppendCallback(() => label.text = string.Empty);
            return showDialogue;
        }
    }
}