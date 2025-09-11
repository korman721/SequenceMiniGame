using Assets._Project.Develop.Runtime.Infrastructer.DI;
using Assets._Project.Develop.Runtime.Utilities.AssetsManagment;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagment;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagment;
using Assets._Project.Develop.Runtime.Utilities.SceneManagment;
using Object = UnityEngine.Object;

namespace Assets._Project.Develop.Runtime.MainMenu.Infrastructer
{
    public class MainMenuContextRegistrations
    {
        public static void Process(DIContainer container)
        {
            container.RegisterAsSingle(CreateSequenceChoiceService);
        }

        private static SequenceChoiceService CreateSequenceChoiceService(DIContainer container)
        {
            ResourcesAssetsLoader resourcesAssetsLoader = container.Resolve<ResourcesAssetsLoader>();

            SequenceChoiceService sequenceChoiceServicePrefab = resourcesAssetsLoader.Load<SequenceChoiceService>("Services/SequenceChoiceService");

            SequenceChoiceService sequenceChoiceService = Object.Instantiate(sequenceChoiceServicePrefab);
            sequenceChoiceService.Initialize(container.Resolve<SceneSwitcherService>(), container.Resolve<ConfigsProviderService>(), container.Resolve<ICoroutinesPerformer>());

            return sequenceChoiceService;
        }
    }
}
