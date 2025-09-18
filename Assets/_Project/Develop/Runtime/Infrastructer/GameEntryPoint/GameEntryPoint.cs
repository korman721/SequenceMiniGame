using Assets._Project.Develop.Runtime.Infrastructer.DI;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagment;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagment;
using Assets._Project.Develop.Runtime.Utilities.DataManagment;
using Assets._Project.Develop.Runtime.Utilities.DataManagment.DataProvider;
using Assets._Project.Develop.Runtime.Utilities.LoadingScreen;
using Assets._Project.Develop.Runtime.Utilities.SceneManagment;
using System.Collections;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Infrastructer.GameEntryPoint
{
    public class GameEntryPoint : MonoBehaviour
    {
        private void Awake()
        {
            SetupAppSettings();

            DIContainer projectContainer = new DIContainer();
            ProjectRegistrationContex.Process(projectContainer);
            projectContainer.Initialize();
            projectContainer.Resolve<ICoroutinesPerformer>().StartPerform(Initialize(projectContainer));
        }

        private void SetupAppSettings()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 60;
        }

        private IEnumerator Initialize(DIContainer container)
        {
            ILoadingScreen loadingScreen = container.Resolve<ILoadingScreen>();
            SceneSwitcherService sceneLoaderService = container.Resolve<SceneSwitcherService>();
            PlayerDataProvider playerDataProvider = container.Resolve<PlayerDataProvider>();
            loadingScreen.Show();

            ConfigsProviderService configsProviderService = container.Resolve<ConfigsProviderService>();
            yield return configsProviderService.LoadAsync();

            bool isExistsPlayerData = false;
            yield return playerDataProvider.Exists(result => isExistsPlayerData = result);

            if (isExistsPlayerData)
                yield return playerDataProvider.Load();
            else
                playerDataProvider.Reset();

            loadingScreen.Hide();

            yield return sceneLoaderService.ProcessSwitchTo(Scenes.MainMenuScene);
        }
    }
}
