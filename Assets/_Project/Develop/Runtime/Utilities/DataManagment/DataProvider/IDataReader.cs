using Assets._Project.Develop.Runtime.Utilities.DataManagment.DataKeysStorage;

namespace Assets._Project.Develop.Runtime.Utilities.DataManagment.DataProvider
{
    public interface IDataReader<TData> where TData : ISaveData
    {
        void ReadFrom(TData data);
    }
}
