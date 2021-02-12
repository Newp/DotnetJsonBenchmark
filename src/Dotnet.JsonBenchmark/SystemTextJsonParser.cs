
using System;
using System.Text;
using System.Text.Json;

namespace Dotnet.JsonBenchmark
{
    public class SystemTextJsonParser : IJsonParser
    {
        public string Name => "System.Text.Json";
        public ConsoleColor BarColor => ConsoleColor.Blue;

        public T DeserializeFromString<T>(string json) => JsonSerializer.Deserialize<T>(json);

        public T DeserializeFromUtf8<T>(byte[] utf8json)
        {
            var json = Encoding.UTF8.GetString(utf8json);
            return JsonSerializer.Deserialize<T>(json); ;
        }
        public string SerializeToString<T>(T instance) => JsonSerializer.Serialize(instance);

        public byte[] SerializeToUtf8<T>(T instance)
        {
            var json = JsonSerializer.Serialize(instance);
            return Encoding.UTF8.GetBytes(json);
        }
    }
}
