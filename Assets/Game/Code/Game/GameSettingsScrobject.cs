using UnityEngine;

namespace Game
{
    [CreateAssetMenu(menuName = "Game/Settings", fileName = "GameSettings")]
    public class GameSettingsScrobject : ScriptableObject
    {
        public CharacterEntry character;
        public RoadEntry road;
    }
}