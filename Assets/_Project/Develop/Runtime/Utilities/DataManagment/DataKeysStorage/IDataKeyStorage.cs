namespace Assets._Project.Develop.Runtime.Utilities.DataManagment.DataKeysStorage
{
    public interface IDataKeyStorage
    {
        string GetKeyFor<TData>() where TData : ISaveData;
    }
}
