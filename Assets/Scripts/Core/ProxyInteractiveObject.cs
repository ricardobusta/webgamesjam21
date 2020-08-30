using System;
using UnityEngine;

namespace Core
{
    public class ProxyInteractiveObject : InteractiveEntity
    {
        [NonSerialized] public InteractiveObject mainObject;

        public override string Name => mainObject.Name;

        public override InteractiveObject MainObject => mainObject;
    }
}