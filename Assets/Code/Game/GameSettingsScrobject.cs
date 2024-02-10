using Game.UI;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(menuName = "Game/Settings", fileName = "GameSettings")]
    public class GameSettingsScrobject : ScriptableObject
    {
        public CharacterSettings character;
        public PlayerSettings player;

        [Space]
        public StartWindowLayout startWindow;
    }
}