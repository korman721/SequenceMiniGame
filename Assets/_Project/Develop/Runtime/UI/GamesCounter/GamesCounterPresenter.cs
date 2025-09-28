using Assets._Project.Develop.Runtime.UI.CommonViews;
using Assets._Project.Develop.Runtime.UI.Core;
using Assets._Project.Develop.Runtime.UI.MainMenu;
using Assets._Project.Develop.Runtime.Utilities.GamesManagment.GamesCounterService;
using System.Collections.Generic;

namespace Assets._Project.Develop.Runtime.UI.GamesCounter
{
    public class GamesCounterPresenter : IPresenter
    {
        private const string CommonTextWins = "Wins: ";
        private const string CommonTextLosses = "Losses: ";

        private readonly ViewsFactory _viewsFactory;
        private readonly MainMenuPresentersFactory _mainMenuPresentersFactory;
        private readonly IGamesCounter _gamesCounterService;
        private readonly ListTextView _gamesTextView;

        private Dictionary<MainMenuGameTypePresenter, TextView> _presenterToView = new();

        public GamesCounterPresenter(
            ViewsFactory viewsFactory,
            MainMenuPresentersFactory mainMenuPresentersFactory,
            IGamesCounter gamesCounterService,
            ListTextView gamesTextView)
        {
            _viewsFactory = viewsFactory;
            _mainMenuPresentersFactory = mainMenuPresentersFactory;
            _gamesCounterService = gamesCounterService;
            _gamesTextView = gamesTextView;
        }

        public void Initialize()
        {
            TextView winsView = _viewsFactory.CreateView<TextView>(ViewID.TextView);
            _gamesTextView.Add(winsView);
            MainMenuGameTypePresenter winsPresenter = _mainMenuPresentersFactory
                .CreateMainMenuGameTypePresenter(winsView, _gamesCounterService.Victories, CommonTextWins);
            winsPresenter.Initialize();

            _presenterToView.Add(winsPresenter, winsView);

            TextView lossesView = _viewsFactory.CreateView<TextView>(ViewID.TextView);
            _gamesTextView.Add(lossesView);
            MainMenuGameTypePresenter lossesPresenter = _mainMenuPresentersFactory
                .CreateMainMenuGameTypePresenter(lossesView, _gamesCounterService.Losses, CommonTextLosses);
            lossesPresenter.Initialize();

            _presenterToView.Add(lossesPresenter, lossesView);
        }

        public void Dispose()
        {
            foreach (KeyValuePair<MainMenuGameTypePresenter, TextView> presenterToView in _presenterToView)
            {
                _gamesTextView.Remove(presenterToView.Value);
                _viewsFactory.Release(presenterToView.Value);
                presenterToView.Key.Dispose();
            }

            _presenterToView.Clear();
        }
    }
}
