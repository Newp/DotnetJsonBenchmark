using Newtonsoft.Json;
using System;
using System.Text;

namespace Dotnet.JsonBenchmark
{
    public class NewtonsoftJsonParser : IJsonParser
    {
        public string Name => "Newtonsoft.Json";
        public ConsoleColor BarColor => ConsoleColor.Yellow;

        public T DeserializeFromString<T>(string json) => JsonConvert.DeserializeObject<T>(json);

        public T DeserializeFromUtf8<T>(byte[] utf8json)
        {
            var json = Encoding.UTF8.GetString(utf8json);
            return JsonConvert.DeserializeObject<T>(json); ;
        }
        public string SerializeToString<T>(T instance) => JsonConvert.SerializeObject(instance);

        public byte[] SerializeToUtf8<T>(T instance)
        {
            var json = JsonConvert.SerializeObject(instance);
            return Encoding.UTF8.GetBytes(json);
        }
    }
}
