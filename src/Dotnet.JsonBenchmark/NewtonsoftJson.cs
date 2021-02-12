using System;

namespace Dotnet.JsonBenchmark
{
    public class NewtonsoftJson : IJsonParser
    {
        public string Name => "Newtonsoft.Json";

        public T DeserializeFromString<T>(string json) 
        {
            throw new NotImplementedException();
        }

        public T DeserializeFromUtf8<T>(byte[] utf8json)
        {
            throw new NotImplementedException();
        }

        public string SerializeToString(string json)
        {
            throw new NotImplementedException();
        }

        public string SerializeToString<T>(T instance)
        {
            throw new NotImplementedException();
        }

        public byte[] SerializeToUtf8(string json)
        {
            throw new NotImplementedException();
        }

        public byte[] SerializeToUtf8<T>(T instance)
        {
            throw new NotImplementedException();
        }
    }
}
