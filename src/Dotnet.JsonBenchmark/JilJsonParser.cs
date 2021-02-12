
using System;
using System.Text;
using Jil;

namespace Dotnet.JsonBenchmark
{
    public class JilJsonParser : IJsonParser
    {
        public string Name => "Jil";
        public ConsoleColor BarColor => ConsoleColor.Red;

        public T DeserializeFromString<T>(string json)=> JSON.Deserialize<T>(json);

        public T DeserializeFromUtf8<T>(byte[] utf8json) => JSON.Deserialize<T>(Encoding.UTF8.GetString(utf8json));

        public string SerializeToString<T>(T instance) => JSON.Serialize(instance);

        public byte[] SerializeToUtf8<T>(T instance) => Encoding.UTF8.GetBytes(JSON.Serialize(instance)); 
    }
}
