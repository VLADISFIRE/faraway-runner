using System;
using UnityEngine;

namespace Game
{
    public class CollisionTriggerComponent : MonoBehaviour
    {
        public event Action<ITriggerSource> triggered;

        public void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out TriggerSourceComponent component))
            {
                triggered?.Invoke(component.source);
            }
        }
    }
}