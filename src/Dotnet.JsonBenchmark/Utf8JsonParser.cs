
using System;
using System.Text;
using Utf8Json;

namespace Dotnet.JsonBenchmark
{
    public class Utf8JsonParser : IJsonParser
    {
        public string Name => "Utf8Json";
        public ConsoleColor BarColor => ConsoleColor.Green;

        public T DeserializeFromString<T>(string json)=> JsonSerializer.Deserialize<T>(json);

        public T DeserializeFromUtf8<T>(byte[] utf8json) => JsonSerializer.Deserialize<T>(utf8json);

        public string SerializeToString<T>(T instance) => JsonSerializer.ToJsonString(instance);

        public byte[] SerializeToUtf8<T>(T instance) => JsonSerializer.Serialize(instance);
    }
}
