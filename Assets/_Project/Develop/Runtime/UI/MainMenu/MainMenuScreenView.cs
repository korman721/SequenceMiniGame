using Assets._Project.Develop.Runtime.UI.CommonViews;
using Assets._Project.Develop.Runtime.UI.Core;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Project.Develop.Runtime.UI.MainMenu
{
    public class MainMenuScreenView : MonoBehaviour, IView
    {
        public event Action ResetButtonPressed;

        [field: SerializeField] public ListIconTextView WalletView { get; private set; }
        [field: SerializeField] public ListTextView GamesCounterView { get; private set; }

        [SerializeField] private Button _resetButton;

        private void OnEnable() => _resetButton.onClick.AddListener(OnButtonClicked);

        private void OnDisable() => _resetButton.onClick.RemoveListener(OnButtonClicked);

        private void OnButtonClicked() => ResetButtonPressed?.Invoke();
    }
}
