using UnityEngine;

namespace Game.Code.Core
{
    public class Bootstrap
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        public static void Boot()
        {
            var boot = new GameObject("[Lifecycle]");
            var component = boot.AddComponent<LifecycleComponent>();
            
            component.Initialize(new Initializer());
        }
    }
}