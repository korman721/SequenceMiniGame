using Assets._Project.Develop.Runtime.Gameplay.Utilites;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagment;
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

        private List<IDisposable> _disposables = new List<IDisposable>();

        public GameplayCycle(
            SequenceChecker sequenceChecker,
            SceneSwitcherService sceneSwitcherService,
            ICoroutinesPerformer coroutinesPerformer,
            SequenceGenerator sequenceGenerator)
        {
            _sequenceChecker = sequenceChecker;
            _sceneSwitcherService = sceneSwitcherService;
            _coroutinesPerformer = coroutinesPerformer;
            _sequenceGenerator = sequenceGenerator;
            _sequenceChecker.WrongInput += OnWrongInput;
            _sequenceChecker.SequenceEnded += OnSequenceEnded;

            _disposables.Add(_sequenceChecker);
        }

        private void OnSequenceEnded()
        {
            Debug.Log($"You Win! Press {RestartKeyCode} to exit in main menu");
            GameEnded();
            _coroutinesPerformer.StartPerform(SwitchSceneAfterPressedKey(Scenes.MainMenuScene));
        }

        private void OnWrongInput(KeyCode pressedKey)
        {
            Debug.Log($"You Loose! You pressed {pressedKey}, its wrong input. Press {RestartKeyCode} to restart game");
            GameEnded();
            _coroutinesPerformer.StartPerform(SwitchSceneAfterPressedKey(Scenes.GameplayScene, new GameplayInputArgs(_sequenceGenerator.InputGameplayArgs.SequenceType, _sequenceGenerator.InputGameplayArgs.Symbols)));
        }

        private void GameEnded()
        {
            _sequenceChecker.WrongInput -= OnWrongInput;
            _sequenceChecker.SequenceEnded -= OnSequenceEnded;

            foreach (IDisposable disposable in _disposables)
                disposable.Dispose();
        }

        private IEnumerator SwitchSceneAfterPressedKey(string nameScene, IInputSceneArgs inputSceneArgs = null)
        {
            yield return new WaitUntil(() => Input.GetKeyDown(RestartKeyCode));
            yield return _coroutinesPerformer.StartPerform(_sceneSwitcherService.ProcessSwitchTo(nameScene, inputSceneArgs));
        }
    }
}
