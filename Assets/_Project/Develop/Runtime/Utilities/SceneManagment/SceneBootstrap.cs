using Assets._Project.Develop.Runtime.Infrastructer.DI;
using System.Collections;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Utilities.SceneManagment
{
    public abstract class SceneBootstrap : MonoBehaviour
    {
        public abstract void SceneContextRegistrations(DIContainer container, IInputSceneArgs sceneArgs = null);
        public abstract IEnumerator Initialize();
        public abstract void Run();
    }
}
