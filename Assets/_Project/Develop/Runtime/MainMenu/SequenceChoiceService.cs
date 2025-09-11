using Assets._Project.Develop.Runtime.Gameplay.Infrastructer;
using Assets._Project.Develop.Runtime.Gameplay.Utilites;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagment;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagment;
using Assets._Project.Develop.Runtime.Utilities.SceneManagment;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.MainMenu.Infrastructer
{
    public class SequenceChoiceService : MonoBehaviour
    {
        private const KeyCode NumbersChoice = KeyCode.Alpha1;
        private const KeyCode AlphabetChoice = KeyCode.Alpha2;

        private SceneSwitcherService _sceneSwitcher;
        private ICoroutinesPerformer _coroutinesPerformer;

        public void Initialize(
            SceneSwitcherService sceneSwitcher,
            ICoroutinesPerformer coroutinesPerformer)
        {
            _sceneSwitcher = sceneSwitcher;
            _coroutinesPerformer = coroutinesPerformer;
        }

        private void Update()
        {
            if (Input.GetKeyDown(NumbersChoice))
                _coroutinesPerformer.StartPerform(_sceneSwitcher.ProcessSwitchTo(Scenes.GameplayScene, new GameplayInputArgs(Sequences.Numbers)));

            if (Input.GetKeyDown(AlphabetChoice))
                _coroutinesPerformer.StartPerform(_sceneSwitcher.ProcessSwitchTo(Scenes.GameplayScene, new GameplayInputArgs(Sequences.Alphabet)));
        }
    }
}
