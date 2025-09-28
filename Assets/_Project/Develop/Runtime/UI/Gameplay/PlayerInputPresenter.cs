using Assets._Project.Develop.Runtime.Gameplay.Utilites;
using Assets._Project.Develop.Runtime.UI.CommonViews;
using Assets._Project.Develop.Runtime.UI.Core;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.UI.Gameplay
{
    public class PlayerInputPresenter : IPresenter
    {
        private readonly TextView _view;
        private readonly PlayerInput _playerInput;

        private string _playerInputSequence = "";

        public PlayerInputPresenter(TextView view, PlayerInput playerInput)
        {
            _view = view;
            _playerInput = playerInput;
        }

        public void Initialize()
        {
            _playerInput.KeyDown += OnKeyDown;
        }

        public void Dispose()
        {
            _playerInput.KeyDown -= OnKeyDown;
        }

        private void OnKeyDown(KeyCode code)
        {
            _playerInputSequence += code.ToString().ToLower()[code.ToString().Length - 1];

            _view.SetText(_playerInputSequence);
        }
    }
}
