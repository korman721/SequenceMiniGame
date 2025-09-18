using Newtonsoft.Json;

namespace Assets._Project.Develop.Runtime.Utilities.DataManagment.Serializers
{
    public class JsonSerializer : IDataSerializer
    {
        public TData Deserialize<TData>(string serializedData)
        {
            return JsonConvert.DeserializeObject<TData>(serializedData, new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Auto,
            });
        }

        public string Serialize<TData>(TData data)
        {
            return JsonConvert.SerializeObject(data, new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Auto,
            });
        }
    }
}
