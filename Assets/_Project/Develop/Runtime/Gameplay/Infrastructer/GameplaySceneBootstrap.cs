using Assets._Project.Develop.Runtime.Gameplay.Utilites;
using Assets._Project.Develop.Runtime.Infrastructer.DI;
using Assets._Project.Develop.Runtime.UI.Gameplay;
using Assets._Project.Develop.Runtime.Utilities.SceneManagment;
using System.Collections;

namespace Assets._Project.Develop.Runtime.Gameplay.Infrastructer
{
    public class GameplaySceneBootstrap : SceneBootstrap
    {
        private DIContainer _container;
        private GameplayInputArgs _gameplaySceneArgs;

        public override void SceneContextRegistrations(DIContainer container, IInputSceneArgs sceneArgs = null)
        {
            GameplayContextRegistrations.Process(container);
            container.Initialize();
            _container = container;

            if (sceneArgs is GameplayInputArgs gameplayInputArgs)
                _gameplaySceneArgs = gameplayInputArgs;
        }

        public override IEnumerator Initialize()
        { 
            yield break;
        }

        public override void Run()
        {
            SequenceGenerator sequenceGenerator = _container.Resolve<SequenceGenerator>();
            sequenceGenerator.Initialize(_gameplaySceneArgs);
            string sequence = sequenceGenerator.GenerateSequence();
            _container.Resolve<SequenceChecker>().Initialize(sequence);
        }
    }
}