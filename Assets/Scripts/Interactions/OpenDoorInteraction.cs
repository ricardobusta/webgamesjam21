using DG.Tweening;
using Ui;
using UnityEngine;

namespace Interactions
{
    public class OpenDoorInteraction : Interaction
    {
        private bool _open;

        public Vector3 rotateTo;
        public float duration;

        public float messageDuration;
        public string openMessage;
        public string alreadyOpenMessage;

        public override void Interact()
        {
            if (!_open)
            {
                transform.DOLocalRotate(rotateTo, duration);
                _open = true;

                if (!string.IsNullOrEmpty(openMessage))
                {
                    ShowDialogue.ShowNormal(new Dialogue
                    {
                        message = openMessage,
                        duration = messageDuration
                    });
                }
            }
            else
            {
                if(!string.IsNullOrEmpty(alreadyOpenMessage))
                {
                    ShowDialogue.ShowNormal(new Dialogue
                    {
                        message = alreadyOpenMessage,
                        duration = messageDuration
                    });
                }
            }
        }
    }
}