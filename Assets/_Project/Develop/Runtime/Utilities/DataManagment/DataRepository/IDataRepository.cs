using System;
using System.Collections;


namespace Assets._Project.Develop.Runtime.Utilities.DataManagment.DataRepository
{
    public interface IDataRepository
    {
        IEnumerator Write(string key, string serializedData);
        IEnumerator Read(string key, Action<string> onRead);
        IEnumerator Exists(string key, Action<bool> onExistsResult);
        IEnumerator Remove(string key);
    }
}
