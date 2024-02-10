using System;

namespace Game
{
    public interface IGameInput
    {
        /// <summary>
        /// Event with GameActionInputType (string)
        /// </summary>
        public event Action<string> onAction;
    }
}