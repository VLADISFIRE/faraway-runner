using UnityEngine;

namespace Game.Core
{
    public static class UnityObjectExtensions
    {
        public static T MakePermanent<T>(this T obj) where T : Object
        {
            Object.DontDestroyOnLoad(obj);
            return obj;
        }
    }
}