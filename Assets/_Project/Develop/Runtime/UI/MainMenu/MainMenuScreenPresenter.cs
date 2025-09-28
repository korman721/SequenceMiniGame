using Assets._Project.Develop.Runtime.Configs.Meta;
using Assets._Project.Develop.Runtime.Meta.Wallet;
using Assets._Project.Develop.Runtime.UI.Core;
using Assets._Project.Develop.Runtime.UI.GamesCounter;
using Assets._Project.Develop.Runtime.UI.Wallet;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagment;
using Assets._Project.Develop.Runtime.Utilities.DataManagment.DataProvider;
using Assets._Project.Develop.Runtime.Utilities.GamesManagment.GamesCounterService;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.UI.MainMenu
{
    public class MainMenuScreenPresenter : IPresenter
    {
        private readonly MainMenuScreenView _view;
        private readonly ProjectPresentersFactory _projectPresentersFactory;
        private readonly MainMenuPresentersFactory _mainMenuPresentersFactory;
        private readonly WalletService _walletService;
        private readonly IGamesCounter _gamesCounterService;
        private readonly LossesVictoriesSettingsConfig _lossesVictoriesSettingsConfig;
        private readonly ICoroutinesPerformer _coroutinesPerformer;
        private readonly PlayerDataProvider _playerDataProvider;

        private List<IPresenter> _childPresenters = new();

        public MainMenuScreenPresenter(
            MainMenuScreenView view,
            ProjectPresentersFactory projectPresentersFactory,
            MainMenuPresentersFactory mainMenuPresentersFactory,
            WalletService walletService,
            IGamesCounter gamesCounterService,
            LossesVictoriesSettingsConfig lossesVictoriesSettingsConfig,
            ICoroutinesPerformer coroutinesPerformer,
            PlayerDataProvider playerDataProvider)
        {
            _view = view;
            _projectPresentersFactory = projectPresentersFactory;
            _mainMenuPresentersFactory = mainMenuPresentersFactory;
            _walletService = walletService;
            _gamesCounterService = gamesCounterService;
            _lossesVictoriesSettingsConfig = lossesVictoriesSettingsConfig;
            _coroutinesPerformer = coroutinesPerformer;
            _playerDataProvider = playerDataProvider;
        }

        public void Initialize()
        {
            CreateWallet();
            CreateGamesCounter();

            _view.ResetButtonPressed += ResetGamesCount;

            foreach (IPresenter presenter in _childPresenters)
                presenter.Initialize();
        }

        public void Dispose()
        {
            _view.ResetButtonPressed -= ResetGamesCount;

            foreach (IPresenter presenter in _childPresenters)
                presenter.Dispose();

            _childPresenters.Clear();
        }

        private void ResetGamesCount()
        {
            if (_walletService.GetCurrnecy(CurrencyTypes.Gold).Value < _lossesVictoriesSettingsConfig.ReloadPrice)
                Debug.Log($"You haven't enough gold - {_walletService.GetCurrnecy(CurrencyTypes.Gold).Value} you have");
            else
            {
                _walletService.Spend(CurrencyTypes.Gold, _lossesVictoriesSettingsConfig.ReloadPrice);
                _gamesCounterService.Reset();
                _coroutinesPerformer.StartPerform(_playerDataProvider.Save());
            }
        }

        private void CreateWallet()
        {
            WalletPresenter walletPresenter = _projectPresentersFactory.CreateWalletPresenter(_view.WalletView);
            _childPresenters.Add(walletPresenter);
        }

        private void CreateGamesCounter()
        {
            GamesCounterPresenter gamesCounterPresenter = _mainMenuPresentersFactory.CreateGamesCounterPresenter(_view.GamesCounterView);
            _childPresenters.Add(gamesCounterPresenter);
        }
    }
}
