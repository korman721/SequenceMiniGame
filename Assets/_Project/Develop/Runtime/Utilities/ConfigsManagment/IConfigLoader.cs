using System;
using System.Collections;
using System.Collections.Generic;

namespace Assets._Project.Develop.Runtime.Utilities.ConfigsManagment
{
    public interface IConfigLoader
    {
        IEnumerator LoadAsync(Action<Dictionary<Type, object>> onLoadedConfigs);
    }
}
