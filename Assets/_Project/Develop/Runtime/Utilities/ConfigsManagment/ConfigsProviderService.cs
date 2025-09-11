using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Utilities.ConfigsManagment
{
    public class ConfigsProviderService
    {
        private readonly Dictionary<Type, object> _configs = new();

        private IConfigLoader[] _loaderConfigs;

        public ConfigsProviderService(params IConfigLoader[] loaderConfigs)
            => _loaderConfigs = loaderConfigs;

        public IEnumerator LoadAsync()
        {
            _configs.Clear();

            foreach (IConfigLoader loaderConfig in _loaderConfigs)
                yield return loaderConfig.LoadAsync(loadedConfigs => _configs.AddRange(loadedConfigs));
        }

        public T Get<T>() where T : class
        {
            if (_configs.ContainsKey(typeof(T)))
                return (T)_configs[typeof(T)];

            throw new InvalidOperationException($"Config {typeof(T)} not exist");
        }
    }
}