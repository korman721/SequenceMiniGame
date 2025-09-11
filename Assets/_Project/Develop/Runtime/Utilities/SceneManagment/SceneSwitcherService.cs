using Assets._Project.Develop.Runtime.Infrastructer.DI;
using Assets._Project.Develop.Runtime.Utilities.LoadingScreen;
using System;
using System.Collections;
using Object = UnityEngine.Object;

namespace Assets._Project.Develop.Runtime.Utilities.SceneManagment
{
    public class SceneSwitcherService
    {
        private DIContainer _container;
        private SceneLoaderService _sceneLoader;
        private ILoadingScreen _loadingScreen;

        public SceneSwitcherService(
            DIContainer container,
            SceneLoaderService sceneLoader, 
            ILoadingScreen loadingScreen)
        {
            _container = container;
            _sceneLoader = sceneLoader;
            _loadingScreen = loadingScreen;
        }

        public IEnumerator ProcessSwitchTo(string sceneName, IInputSceneArgs inputSceneArgs = null)
        {
            _loadingScreen.Show();

            yield return _sceneLoader.LoadAsync(Scenes.EmptyScene);
            yield return _sceneLoader.LoadAsync(sceneName);

            SceneBootstrap sceneBootstrap = Object.FindObjectOfType<SceneBootstrap>();

            if (sceneBootstrap == null)
                throw new NullReferenceException(nameof(sceneBootstrap) + " not found");

            DIContainer sceneContainer = new DIContainer(_container);

            sceneBootstrap.SceneContextRegistrations(sceneContainer, inputSceneArgs);

            yield return sceneBootstrap.Initialize();

            _loadingScreen.Hide();

            sceneBootstrap.Run();
        }
    }
}
