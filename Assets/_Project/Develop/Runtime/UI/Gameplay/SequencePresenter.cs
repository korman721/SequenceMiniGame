using Assets._Project.Develop.Runtime.Gameplay.Utilites;
using Assets._Project.Develop.Runtime.UI.CommonViews;
using Assets._Project.Develop.Runtime.UI.Core;
using System;

namespace Assets._Project.Develop.Runtime.UI.Gameplay
{
    public class SequencePresenter : IPresenter
    {
        private readonly TextView _view;
        private readonly SequenceChecker _sequenceChecker;

        public SequencePresenter(TextView view, SequenceChecker sequenceChecker)
        {
            _view = view;
            _sequenceChecker = sequenceChecker;
        }

        public void Initialize() => _sequenceChecker.SequenceInitialized += OnSequenceInitialized;

        public void Dispose() => _sequenceChecker.SequenceInitialized -= OnSequenceInitialized;


        private void OnSequenceInitialized(string sequence) => _view.SetText(sequence);
    }
}
