using Assets._Project.Develop.Runtime.Utilities.DataManagment.DataKeysStorage;
using Assets._Project.Develop.Runtime.Utilities.DataManagment.DataRepository;
using Assets._Project.Develop.Runtime.Utilities.DataManagment.Serializers;
using System;
using System.Collections;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Utilities.DataManagment
{
    public class SaveLoadService : ISaveLoadService
    {
        private IDataSerializer _serializer;
        private IDataKeyStorage _keyStorage;
        private IDataRepository _dataRepository;

        public SaveLoadService(
            IDataSerializer serializer, 
            IDataKeyStorage keyStorage, 
            IDataRepository dataRepository)
        {
            _serializer = serializer;
            _keyStorage = keyStorage;
            _dataRepository = dataRepository;
        }

        public IEnumerator Exists<TData>(Action<bool> onExistsResult) where TData : ISaveData
        {
            yield return _dataRepository.Exists(_keyStorage.GetKeyFor<TData>(), result => onExistsResult?.Invoke(result));
        }

        public IEnumerator Load<TData>(Action<TData> onLoad) where TData : ISaveData
        {
            string serializedData = "";

            yield return _dataRepository.Read(_keyStorage.GetKeyFor<TData>(), data => serializedData = data);

            TData data = _serializer.Deserialize<TData>(serializedData);

            onLoad?.Invoke(data);
        }

        public IEnumerator Remove<TData>() where TData : ISaveData
        {
            yield return _dataRepository.Remove(_keyStorage.GetKeyFor<TData>());
        }

        public IEnumerator Save<TData>(TData data) where TData : ISaveData
        {
            string serializedData = _serializer.Serialize(data);

            yield return _dataRepository.Write(_keyStorage.GetKeyFor<TData>(), serializedData);
        }
    }
}
