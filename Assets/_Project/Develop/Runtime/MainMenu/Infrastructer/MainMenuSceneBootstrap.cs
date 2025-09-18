using Assets._Project.Develop.Runtime.Infrastructer.DI;
using Assets._Project.Develop.Runtime.Utilities.SceneManagment;
using System.Collections;

namespace Assets._Project.Develop.Runtime.MainMenu.Infrastructer
{
    public class MainMenuSceneBootstrap : SceneBootstrap
    {
        public override void SceneContextRegistrations(DIContainer container, IInputSceneArgs sceneArgs = null)
        {
            MainMenuContextRegistrations.Process(container);
            container.Initialize();
        }

        public override IEnumerator Initialize()
        {
            yield break;
        }

        public override void Run()
        {
            
        }
    }
}