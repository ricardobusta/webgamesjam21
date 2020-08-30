using UnityEngine;

namespace Core
{
    public abstract class InteractiveEntity : MonoBehaviour
    {
        public abstract string Name { get; }

        public abstract InteractiveObject MainObject { get; }
    }
}