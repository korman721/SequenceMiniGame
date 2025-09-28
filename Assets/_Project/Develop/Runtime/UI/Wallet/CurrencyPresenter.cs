using Assets._Project.Develop.Runtime.Configs.UI;
using Assets._Project.Develop.Runtime.Meta.Wallet;
using Assets._Project.Develop.Runtime.UI.CommonViews;
using Assets._Project.Develop.Runtime.UI.Core;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using System;

namespace Assets._Project.Develop.Runtime.UI.Wallet
{
    public class CurrencyPresenter : IPresenter
    {
        private readonly IReadonlyVariable<int> _variable;
        private readonly CurrencyTypes _currencyType;
        private readonly IconTextView _view;
        private readonly CurrencySpritesConfig _spritesConfig;

        private IDisposable _disposable;

        public CurrencyPresenter(
            IReadonlyVariable<int> variable,
            CurrencyTypes currencyType,
            IconTextView view,
            CurrencySpritesConfig spritesConfig)
        {
            _variable = variable;
            _currencyType = currencyType;
            _view = view;
            _spritesConfig = spritesConfig;
        }

        public void Initialize()
        {
            UpdateValue(_variable.Value);
            _view.SetImage(_spritesConfig.GetSpriteBy(_currencyType));

            _variable.Subscribe(OnValueChanged);
        }

        public void Dispose() => _disposable.Dispose();

        private void OnValueChanged(int arg1, int newValue) => UpdateValue(newValue);

        private void UpdateValue(int value) => _view.SetText(value.ToString());
    }
}
