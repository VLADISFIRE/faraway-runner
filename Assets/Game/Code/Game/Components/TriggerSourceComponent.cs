using UnityEngine;

namespace Game
{
    public class TriggerSourceComponent : MonoBehaviour
    {
        private ITriggerSource _source;

        public ITriggerSource source { get { return _source; } }

        public void SetSource(ITriggerSource source)
        {
            _source = source;
        }
    }
}