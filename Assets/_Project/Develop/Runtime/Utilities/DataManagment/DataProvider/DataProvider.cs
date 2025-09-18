using Assets._Project.Develop.Runtime.Utilities.DataManagment.DataKeysStorage;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Assets._Project.Develop.Runtime.Utilities.DataManagment.DataProvider
{
    public abstract class DataProvider<TData> where TData : ISaveData
    {
        private ISaveLoadService _saveLoadService;
        private TData _data;

        private List<IDataReader<TData>> _readers = new();
        private List<IDataWriter<TData>> _writers = new();

        protected DataProvider(ISaveLoadService saveLoadService)
        {
            _saveLoadService = saveLoadService;
        }

        public void RegisterReader(IDataReader<TData> reader)
        {
            if (_readers.Contains(reader))
                throw new InvalidOperationException($"{nameof(reader)} already exists in list");

            _readers.Add(reader);
        }

        public void RegisterWriter(IDataWriter<TData> writer)
        {
            if (_writers.Contains(writer))
                throw new InvalidOperationException($"{nameof(writer)} already exists in list");

            _writers.Add(writer);
        }

        public IEnumerator Load()
        {
            yield return _saveLoadService.Load<TData>(loadedData => _data = loadedData);

            SendDataToReaders();
        }

        public IEnumerator Save()
        {
            GetDataFromWriters();

            yield return _saveLoadService.Save(_data);
        }

        public IEnumerator Exists(Action<bool> onExistsResult)
        {
            yield return _saveLoadService.Exists<TData>(result => onExistsResult?.Invoke(result));
        }

        public IEnumerator Remove()
        {
            yield return _saveLoadService.Remove<TData>();
        }

        public void Reset()
        {
            _data = GetOriginData();

            SendDataToReaders();
        }

        public abstract TData GetOriginData();

        private void SendDataToReaders()
        {
            foreach (IDataReader<TData> reader in _readers)
                reader.ReadFrom(_data);
        }

        private void GetDataFromWriters()
        {
            foreach (IDataWriter<TData> writer in _writers)
                writer.WriteTo(_data);
        }
    }
}
