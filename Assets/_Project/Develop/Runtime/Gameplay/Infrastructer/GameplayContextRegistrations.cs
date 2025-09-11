using Assets._Project.Develop.Runtime.Gameplay.Utilites;
using Assets._Project.Develop.Runtime.Infrastructer.DI;
using Assets._Project.Develop.Runtime.Utilities.AssetsManagment;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagment;
using Assets._Project.Develop.Runtime.Utilities.SceneManagment;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Infrastructer
{
    public class GameplayContextRegistrations
    {
        public static void Process(DIContainer container)
        {
            container.RegisterAsSingle(CreateSequenceGenerator);
            container.RegisterAsSingle(CreatePlayerInput);
            container.RegisterAsSingle(CreateSequenceChecker);
            container.RegisterAsSingle(CreateGameplayCycle);
        }

        private static SequenceGenerator CreateSequenceGenerator(DIContainer container)
            => new SequenceGenerator();

        private static PlayerInput CreatePlayerInput(DIContainer container)
        {
            ResourcesAssetsLoader resourcesAssetsLoader = container.Resolve<ResourcesAssetsLoader>();

            PlayerInput playerInput = resourcesAssetsLoader.Load<PlayerInput>("Services/PlayerInput");

            return Object.Instantiate(playerInput);
        }

        private static SequenceChecker CreateSequenceChecker(DIContainer container)
            => new SequenceChecker(container.Resolve<PlayerInput>());

        private static GameplayCycle CreateGameplayCycle(DIContainer container)
            => new GameplayCycle(
                container.Resolve<SequenceChecker>(),
                container.Resolve<SceneSwitcherService>(),
                container.Resolve<ICoroutinesPerformer>(),
                container.Resolve<SequenceGenerator>());
    }
}
