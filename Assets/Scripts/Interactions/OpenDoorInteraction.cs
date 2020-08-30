using DG.Tweening;
using UnityEngine;

namespace Interactions
{
    public class OpenDoorInteraction : Interaction
    {
        private bool _open;

        public Vector3 rotateTo;
        public float duration;

        public override void Interact()
        {
            if (!_open)
            {
                transform.DOLocalRotate(rotateTo, duration);
                _open = true;
            }
        }
    }
}