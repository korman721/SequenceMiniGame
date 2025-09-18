using System;
using System.Collections;
using System.IO;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Utilities.DataManagment.DataRepository
{
    public class LocalDataRepository : IDataRepository
    {
        private string _folderPath;
        private string _fileExtension;

        public LocalDataRepository(string folderPath, string fileExtension)
        {
            _folderPath = folderPath;
            _fileExtension = fileExtension;
        }

        public IEnumerator Exists(string key, Action<bool> onExistsResult)
        {
            bool exists = File.Exists(GetPathFor(key));
            onExistsResult?.Invoke(exists);

            yield break;
        }

        public IEnumerator Read(string key, Action<string> onRead)
        {
            string serializedData = File.ReadAllText(GetPathFor(key));
            onRead?.Invoke(serializedData);

            yield break;
        }

        public IEnumerator Remove(string key)
        {
            File.Delete(GetPathFor(key));

            yield break;
        }

        public IEnumerator Write(string key, string serializedData)
        {
            File.WriteAllText(GetPathFor(key), serializedData);

            yield break;
        }

        private string GetPathFor(string key) 
            => Path.Combine(_folderPath, key) + "." + _fileExtension;
    }
}
