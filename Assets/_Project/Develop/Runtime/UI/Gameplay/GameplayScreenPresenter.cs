using Assets._Project.Develop.Runtime.Gameplay.Utilites;
using Assets._Project.Develop.Runtime.UI.Core;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.UI.Gameplay
{
    public class GameplayScreenPresenter : IPresenter
    {
        private readonly GameplayScreenView _view;
        private readonly GameplayPresentersFactory _gameplayPresentersFactory;

        private List<IPresenter> _childPresenters = new();

        public GameplayScreenPresenter(
            GameplayScreenView view,
            GameplayPresentersFactory gameplayPresentersFactory,
            SequenceChecker sequenceChecker)
        {
            _view = view;
            _gameplayPresentersFactory = gameplayPresentersFactory;

            sequenceChecker.WrongInput += OnWrongInput;
            sequenceChecker.SequenceEnded += OnSequenceEnded;
        }

        public void Initialize()
        {
            CreatePlayerInputPresenter();
            CreateSequencrPresenter();

            foreach (IPresenter presenter in _childPresenters)
                presenter.Initialize();
        }

        public void Dispose()
        {
            foreach (IPresenter presenter in _childPresenters)
                presenter.Dispose();

            _childPresenters.Clear();
        }

        private void CreatePlayerInputPresenter()
        {
            PlayerInputPresenter playerInputPresenter = _gameplayPresentersFactory.CreatePlayerInputPresenter(_view.PlayerInputView);
            _childPresenters.Add(playerInputPresenter);
        }

        private void CreateSequencrPresenter()
        {
            SequencePresenter sequencePresenter = _gameplayPresentersFactory.CreateSequencePresenter(_view.SequenceView);
            _childPresenters.Add(sequencePresenter);
        }

        private void OnSequenceEnded() => Dispose();

        private void OnWrongInput(KeyCode code) => Dispose();
    }
}
