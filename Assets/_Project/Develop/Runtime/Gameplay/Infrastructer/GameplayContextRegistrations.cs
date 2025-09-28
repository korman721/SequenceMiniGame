using Assets._Project.Develop.Runtime.Gameplay.Utilites;
using Assets._Project.Develop.Runtime.Infrastructer.DI;
using Assets._Project.Develop.Runtime.Meta.Wallet;
using Assets._Project.Develop.Runtime.UI.Core;
using Assets._Project.Develop.Runtime.UI.Gameplay;
using Assets._Project.Develop.Runtime.UI.MainMenu;
using Assets._Project.Develop.Runtime.Utilities.AssetsManagment;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagment;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagment;
using Assets._Project.Develop.Runtime.Utilities.DataManagment.DataProvider;
using Assets._Project.Develop.Runtime.Utilities.GamesManagment.GamesCounterService;
using Assets._Project.Develop.Runtime.Utilities.SceneManagment;
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
            container.RegisterAsSingle(CreateGameplayCycle).NonLazy();
            container.RegisterAsSingle(CreateGameplayUIRoot);
            container.RegisterAsSingle(CreateGameplayPresentersFactory);
            container.RegisterAsSingle(CreateGameplayScreenPresenter).NonLazy();
        }

        private static GameplayPresentersFactory CreateGameplayPresentersFactory(DIContainer container) =>
            new GameplayPresentersFactory(container); 

        private static GameplayUIRoot CreateGameplayUIRoot(DIContainer container)
        {
            ResourcesAssetsLoader resourcesAssetsLoader = container.Resolve<ResourcesAssetsLoader>();

            GameplayUIRoot gameplayUIRootPrefab = resourcesAssetsLoader.Load<GameplayUIRoot>("UI/Gameplay/GameplayUIRoot");

            return Object.Instantiate(gameplayUIRootPrefab);
        }

        private static GameplayScreenPresenter CreateGameplayScreenPresenter(DIContainer container)
        {
            GameplayUIRoot gameplayUIRoot = container.Resolve<GameplayUIRoot>();

            GameplayScreenView view = container.Resolve<ViewsFactory>()
                .CreateView<GameplayScreenView>(ViewID.GameplayScreenView, gameplayUIRoot.HUDLayer);

            GameplayScreenPresenter presenter = container.Resolve<GameplayPresentersFactory>().CreateGameplayScreenPresenter(view);

            return presenter;
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
                container.Resolve<SequenceGenerator>(),
                container.Resolve<IGamesCounter>(),
                container.Resolve<WalletService>(),
                container.Resolve<ConfigsProviderService>(),
                container.Resolve<PlayerDataProvider>());
    }
}
