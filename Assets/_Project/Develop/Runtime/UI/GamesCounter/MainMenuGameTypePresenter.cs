using Assets._Project.Develop.Runtime.UI.CommonViews;
using Assets._Project.Develop.Runtime.UI.Core;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using System;

namespace Assets._Project.Develop.Runtime.UI.GamesCounter
{
    public class MainMenuGameTypePresenter : IPresenter
    {
        private readonly TextView _view;
        private readonly IReadonlyVariable<int> _games;
        private readonly string _commonText;

        private IDisposable _disposable;

        public MainMenuGameTypePresenter(
            TextView view,
            IReadonlyVariable<int> games,
            string commonText = null)
        {
            _view = view;
            _games = games;
            _commonText = commonText;
        }

        public void Initialize()
        {
            UpdateValue(_games.Value);

            _disposable = _games.Subscribe(OnGamesChanged);
        }

        public void Dispose() => _disposable.Dispose();


        private void OnGamesChanged(int arg1, int newValue) => UpdateValue(newValue);

        private void UpdateValue(int newValue) => _view.SetText(_commonText + newValue.ToString());
    }
}
