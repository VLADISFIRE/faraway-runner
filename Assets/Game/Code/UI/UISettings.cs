using System;
using UnityEngine;

namespace Game.UI
{
    [CreateAssetMenu(menuName = "Game/UI/Settings", fileName = "UISettings")]
    public class UISettingsScrobject : ScriptableObject
    {
        public UIWindowsPrefabs windows;
    }

    [Serializable]
    public class UIWindowsPrefabs
    {
        public StartWindowLayout start;
    }
}