using System;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Utilites
{
    public class PlayerInput : MonoBehaviour
    {
        public event Action<KeyCode> KeyDown;

        private readonly Array KeyCodes = Enum.GetValues(typeof(KeyCode));

        private void Update()
        {
            foreach (KeyCode keyCode in KeyCodes)
                if (Input.GetKeyDown(keyCode))
                    KeyDown?.Invoke(keyCode);
        }
    }
}
