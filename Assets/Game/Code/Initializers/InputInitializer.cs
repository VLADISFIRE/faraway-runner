using Game.InputManagement;

namespace Game
{
    public class InputInitializer
    {
        public static void Initialize()
        {
            IInputReader inputReader;
#if UNITY_EDITOR
            inputReader = new DesktopBaseInputReader();
#else
            _inputReader = new MobileInputReader();
#endif
            ServiceLocator.Register(inputReader);

            ServiceLocator.Register<IGameInput>(new GameInput());
        }
    }
}