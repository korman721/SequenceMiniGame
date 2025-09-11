using UnityEngine;

namespace Assets._Project.Develop.Runtime.Utilities.AssetsManagment
{
    public class ResourcesAssetsLoader
    {
        public T Load<T>(string path) where T : Object
            => Resources.Load<T>(path);
    }
}