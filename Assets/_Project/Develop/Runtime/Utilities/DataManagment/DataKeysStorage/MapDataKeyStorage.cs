using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets._Project.Develop.Runtime.Utilities.DataManagment.DataKeysStorage
{
    public class MapDataKeyStorage : IDataKeyStorage
    {
        private readonly Dictionary<Type, string> keys = new()
        {
            {typeof(PlayerData), "PlayerData"}
        };

        public string GetKeyFor<TData>() where TData : ISaveData
        {
            if (keys.ContainsKey(typeof(TData)) == false)
                throw new InvalidOperationException(typeof(TData) + "key not exists");

            return keys[typeof(TData)];
        }
    }
}
