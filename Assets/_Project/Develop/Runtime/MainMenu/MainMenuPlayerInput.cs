using Assets._Project.Develop.Runtime.Configs;
using Assets._Project.Develop.Runtime.Configs.Meta;
using Assets._Project.Develop.Runtime.Gameplay.Infrastructer;
using Assets._Project.Develop.Runtime.Meta.Wallet;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagment;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagment;
using Assets._Project.Develop.Runtime.Utilities.DataManagment.DataProvider;
using Assets._Project.Develop.Runtime.Utilities.GamesManagment.GamesCounterService;
using Assets._Project.Develop.Runtime.Utilities.SceneManagment;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.MainMenu.Infrastructer
{
    public class MainMenuPlayerInput : MonoBehaviour
    {
        private const KeyCode NumbersChoice = KeyCode.Alpha1;
        private const KeyCode AlphabetChoice = KeyCode.Alpha2;
        private const KeyCode SeeInformation = KeyCode.E;
        private const KeyCode Reload = KeyCode.F;

        private SceneSwitcherService _sceneSwitcher;
        private ConfigsProviderService _configsProviderService;
        private ICoroutinesPerformer _coroutinesPerformer;
        private IGamesCounter _gamesCounterService;
        private WalletService _walletService;
        private PlayerDataProvider _playerDataProvider;
        private LossesVictoriesSettingsConfig _lossesVictoriesSettingsConfig;

        public void Initialize(
            SceneSwitcherService sceneSwitcher,
            ConfigsProviderService configsProviderService,
            ICoroutinesPerformer coroutinesPerformer,
            IGamesCounter gamesCounter,
            WalletService walletService,
            PlayerDataProvider playerDataProvider)
        {
            _sceneSwitcher = sceneSwitcher;
            _configsProviderService = configsProviderService;
            _coroutinesPerformer = coroutinesPerformer;
            _gamesCounterService = gamesCounter;
            _walletService = walletService;
            _playerDataProvider = playerDataProvider;
            _lossesVictoriesSettingsConfig = _configsProviderService.Get<LossesVictoriesSettingsConfig>();

            Debug.Log($"Press {NumbersChoice} to play with sequence of numbers. Press {AlphabetChoice} to play with sequence of alphabet");
            Debug.Log($"Also press {SeeInformation} to see your gameplay stats, and press {Reload} to reset your games, for price {_lossesVictoriesSettingsConfig.ReloadPrice}");
        }

        private void Update()
        {
            if (Input.GetKeyDown(NumbersChoice))
                _coroutinesPerformer.StartPerform(_sceneSwitcher.ProcessSwitchTo(
                    Scenes.GameplayScene, 
                    new GameplayInputArgs(_configsProviderService
                    .Get<SequenceNumbersConfig>().SequenceType, _configsProviderService
                    .Get<SequenceNumbersConfig>().Symbols)));

            if (Input.GetKeyDown(AlphabetChoice))
                _coroutinesPerformer.StartPerform(_sceneSwitcher.ProcessSwitchTo(
                    Scenes.GameplayScene,
                    new GameplayInputArgs(_configsProviderService.
                    Get<SequenceAlphabetConfig>().SequenceType, _configsProviderService.
                    Get<SequenceAlphabetConfig>().Symbols)));

            if (Input.GetKeyDown(SeeInformation))
                GetInformation();

            if (Input.GetKeyDown(Reload))
                RealodGamesCount();
        }

        private void GetInformation()
        {
            Debug.Log($"Your victories - {_gamesCounterService.Victories}, losses - {_gamesCounterService.Losses}");
            Debug.Log($"Your gold count - {_walletService.GetCurrnecy(CurrencyTypes.Gold).Value}");
        }

        private void RealodGamesCount()
        {
            if (_walletService.GetCurrnecy(CurrencyTypes.Gold).Value < _lossesVictoriesSettingsConfig.ReloadPrice)
                Debug.Log($"You haven't enough gold - {_walletService.GetCurrnecy(CurrencyTypes.Gold).Value} you have");
            else
            {
                _walletService.Spend(CurrencyTypes.Gold, _lossesVictoriesSettingsConfig.ReloadPrice);
                _gamesCounterService.Reset();
                _coroutinesPerformer.StartPerform(_playerDataProvider.Save());
                Debug.Log("Games reseted");
            }
        }
    }
}
