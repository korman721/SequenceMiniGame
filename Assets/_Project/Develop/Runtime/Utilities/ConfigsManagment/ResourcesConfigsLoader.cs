using Assets._Project.Develop.Runtime.Configs;
using Assets._Project.Develop.Runtime.Configs.Meta;
using Assets._Project.Develop.Runtime.Utilities.AssetsManagment;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Utilities.ConfigsManagment
{
    public class ResourcesConfigsLoader : IConfigLoader
    {
        private ResourcesAssetsLoader _resources;

        private readonly Dictionary<Type, string> _configsPaths = new Dictionary<Type, string>()
        {
            { typeof(SequenceAlphabetConfig), "Configs/Gameplay/SequenceAlphabetConfig"},
            { typeof(SequenceNumbersConfig), "Configs/Gameplay/SequenceNumbersConfig"},
            { typeof(StartWalletConfig), "Configs/Meta/StartWalletConfig"},
            { typeof(LossesVictoriesSettingsConfig), "Configs/Meta/LossesVictoriesSettingsConfig"}
        };

        public ResourcesConfigsLoader(ResourcesAssetsLoader resources) => _resources = resources;

        public IEnumerator LoadAsync(Action<Dictionary<Type, object>> onLoadedConfigs)
        {
            Dictionary<Type, object> loadedConfigs = new();

            foreach (KeyValuePair<Type, string> configsPaths in _configsPaths)
            {
                ScriptableObject config = _resources.Load<ScriptableObject>(configsPaths.Value);
                loadedConfigs.Add(configsPaths.Key, config);
                yield return null;
            }

            onLoadedConfigs?.Invoke(loadedConfigs);
        }
    }
}
