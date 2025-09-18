using Assets._Project.Develop.Runtime.Infrastructer.DI;
using Assets._Project.Develop.Runtime.Meta.Wallet;
using Assets._Project.Develop.Runtime.Utilities.AssetsManagment;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagment;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagment;
using Assets._Project.Develop.Runtime.Utilities.DataManagment.DataProvider;
using Assets._Project.Develop.Runtime.Utilities.GamesManagment.GamesCounterService;
using Assets._Project.Develop.Runtime.Utilities.SceneManagment;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets._Project.Develop.Runtime.MainMenu.Infrastructer
{
    public class MainMenuContextRegistrations
    {
        public static void Process(DIContainer container)
        {
            container.RegisterAsSingle(CreateSequenceChoiceService).NonLazy();
        }

        private static MainMenuPlayerInput CreateSequenceChoiceService(DIContainer container)
        {
            ResourcesAssetsLoader resourcesAssetsLoader = container.Resolve<ResourcesAssetsLoader>();

            MainMenuPlayerInput sequenceChoiceServicePrefab = resourcesAssetsLoader.Load<MainMenuPlayerInput>("Services/MainMenuPlayerInput");

            MainMenuPlayerInput sequenceChoiceService = Object.Instantiate(sequenceChoiceServicePrefab);
            sequenceChoiceService.Initialize(
                container.Resolve<SceneSwitcherService>(),
                container.Resolve<ConfigsProviderService>(),
                container.Resolve<ICoroutinesPerformer>(),
                container.Resolve<IGamesCounter>(),
                container.Resolve<WalletService>(),
                container.Resolve<PlayerDataProvider>());

            return sequenceChoiceService;
        }
    }
}
