namespace Assets._Project.Develop.Runtime.Utilities.DataManagment.Serializers
{
    public interface IDataSerializer
    {
        string Serialize<TData>(TData data);

        TData Deserialize<TData>(string serializedData);
    }
}
