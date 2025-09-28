using System;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Utilites
{
    public class SequenceChecker : IDisposable
    {
        public event Action<string> SequenceInitialized;
        public event Action SequenceEnded;
        public event Action<KeyCode> WrongInput;

        private PlayerInput _playerInput;

        private string _sequence;

        public SequenceChecker(PlayerInput playerInput)
        {
            _playerInput = playerInput;
            _playerInput.KeyDown += OnKeyDown;
        }

        public void Dispose() => _playerInput.KeyDown -= OnKeyDown;

        public void Initialize(string sequence)
        {
            _sequence = sequence;
            SequenceInitialized?.Invoke(_sequence);
        }

        private void OnKeyDown(KeyCode key)
        {
            if (_sequence[0] == key.ToString().ToLower()[key.ToString().Length - 1])
                _sequence = _sequence.Substring(1);
            else
                WrongInput?.Invoke(key);

            if (_sequence.Length == 0)
                SequenceEnded?.Invoke();
        }
    }
}