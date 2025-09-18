using Assets._Project.Develop.Runtime.Configs.Meta;
using Assets._Project.Develop.Runtime.Gameplay.Utilites;
using Assets._Project.Develop.Runtime.Meta.Wallet;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagment;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagment;
using Assets._Project.Develop.Runtime.Utilities.DataManagment.DataProvider;
using Assets._Project.Develop.Runtime.Utilities.GamesManagment.GamesCounterService;
using Assets._Project.Develop.Runtime.Utilities.SceneManagment;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Infrastructer
{
    public class GameplayCycle
    {
        private const KeyCode RestartKeyCode = KeyCode.Space;

        private SequenceChecker _sequenceChecker;
        private SceneSwitcherService _sceneSwitcherService;
        private ICoroutinesPerformer _coroutinesPerformer;
        private SequenceGenerator _sequenceGenerator;
        private IGamesCounter _gamesCounterService;
        private WalletService _walletService;
        private LossesVictoriesSettingsConfig _lossesVictoriesSettingsConfig;
        private PlayerDataProvider _playerDataProvider;

        private List<IDisposable> _disposables = new List<IDisposable>();

        public GameplayCycle(
            SequenceChecker sequenceChecker,
            SceneSwitcherService sceneSwitcherService,
            ICoroutinesPerformer coroutinesPerformer,
            SequenceGenerator sequenceGenerator,
            IGamesCounter gamesCounterService,
            WalletService walletService,
            ConfigsProviderService configProviderService,
            PlayerDataProvider playerDataProvider)
        {
            _sequenceChecker = sequenceChecker;
            _sceneSwitcherService = sceneSwitcherService;
            _coroutinesPerformer = coroutinesPerformer;
            _sequenceGenerator = sequenceGenerator;
            _gamesCounterService = gamesCounterService;
            _walletService = walletService;
            _lossesVictoriesSettingsConfig = configProviderService.Get<LossesVictoriesSettingsConfig>();
            _playerDataProvider = playerDataProvider;
            _sequenceChecker.WrongInput += OnWrongInput;
            _sequenceChecker.SequenceEnded += OnSequenceEnded;

            _disposables.Add(_sequenceChecker);
        }

        private void OnSequenceEnded()
        {
            Win();
            GameEnded();
            _coroutinesPerformer.StartPerform(SwitchSceneAfterPressedKey(Scenes.MainMenuScene));
        }

        private void OnWrongInput(KeyCode pressedKey)
        {
            Loss(pressedKey);
            GameEnded();
            _coroutinesPerformer.StartPerform(SwitchSceneAfterPressedKey(Scenes.GameplayScene, pressedKey, new GameplayInputArgs(_sequenceGenerator.InputGameplayArgs.SequenceType, _sequenceGenerator.InputGameplayArgs.Symbols)));
        }

        private void GameEnded()
        {
            _sequenceChecker.WrongInput -= OnWrongInput;
            _sequenceChecker.SequenceEnded -= OnSequenceEnded;

            foreach (IDisposable disposable in _disposables)
                disposable.Dispose();
        }

        private IEnumerator SwitchSceneAfterPressedKey(string nameScene, KeyCode pressedKey = KeyCode.None, IInputSceneArgs inputSceneArgs = null)
        {
            if (pressedKey == RestartKeyCode)
                yield return new WaitUntil(() => Input.GetKeyDown(RestartKeyCode));

            yield return new WaitUntil(() => Input.GetKeyDown(RestartKeyCode));
            yield return _coroutinesPerformer.StartPerform(_sceneSwitcherService.ProcessSwitchTo(nameScene, inputSceneArgs));
        }

        private void Win()
        {
            Debug.Log($"You Win! Press {RestartKeyCode} to exit in main menu");
            _walletService.Add(CurrencyTypes.Gold, _lossesVictoriesSettingsConfig.AddGoldForVictory);
            _gamesCounterService.Victory();
            _coroutinesPerformer.StartPerform(_playerDataProvider.Save());
        }

        private void Loss(KeyCode pressedKey)
        {
            Debug.Log($"You Loose! You pressed {pressedKey}, its wrong input. Press {RestartKeyCode} to restart game");
            _walletService.Spend(CurrencyTypes.Gold, _lossesVictoriesSettingsConfig.SpendGoldForLoss);
            _gamesCounterService.Loss();
            _coroutinesPerformer.StartPerform(_playerDataProvider.Save());
        }
    }
}
