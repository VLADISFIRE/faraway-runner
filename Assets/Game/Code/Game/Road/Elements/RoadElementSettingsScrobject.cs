using UnityEngine;

namespace Game
{
    [CreateAssetMenu(menuName = "Game/Road/Element", fileName = "New RoadElement")]
    public class RoadElementSettingsScrobject : ScriptableObject
    {
        public RoadElementEntry entry;
    }
}