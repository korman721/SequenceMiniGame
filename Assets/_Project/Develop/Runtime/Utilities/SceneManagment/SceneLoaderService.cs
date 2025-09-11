using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Utilities.SceneManagment
{
    public class SceneLoaderService
    {
        public IEnumerator LoadAsync(string sceneName, LoadSceneMode loadSceneMode = LoadSceneMode.Single)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName, loadSceneMode);

            yield return new WaitWhile(() => operation.isDone == false);
        }

        public IEnumerator UnloadAsync(string sceneName)
        {
            AsyncOperation operation = SceneManager.UnloadSceneAsync(sceneName);

            yield return new WaitWhile(() => operation.isDone == false);
        }
    }
}
