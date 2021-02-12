using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Dotnet.JsonBenchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            


        }

        static Dictionary<string, Dictionary<string, decimal>> Benchmark<T>(T instance, int testCount)
        {
            var serializers = Assembly
                .GetCallingAssembly()
                .GetTypes()
                .Where(type => type.IsInterface == false && typeof(IJsonParser).IsAssignableFrom(type))
                .Select(type => (IJsonParser)Activator.CreateInstance(type));

            Dictionary<string, Dictionary<string, decimal>> results = new();

            results.Add(nameof(IJsonParser.SerializeToString), new());
            results.Add(nameof(IJsonParser.DeserializeFromString), new());
            results.Add(nameof(IJsonParser.SerializeToUtf8), new());
            results.Add(nameof(IJsonParser.DeserializeFromUtf8), new());

            foreach (var serializer in serializers)
            {
                var json = serializer.SerializeToString(instance);

                results[nameof(IJsonParser.SerializeToString)].Add(serializer.Name, Check(testCount, () => serializer.SerializeToString(instance)));
                results[nameof(IJsonParser.DeserializeFromString)].Add(serializer.Name, Check(testCount, () => serializer.DeserializeFromString<T>(json)));


                var utf8 = serializer.SerializeToUtf8(instance);

                results[nameof(IJsonParser.SerializeToUtf8)].Add(serializer.Name, Check(testCount, () => serializer.SerializeToUtf8(instance)));
                results[nameof(IJsonParser.DeserializeFromUtf8)].Add(serializer.Name, Check(testCount, () => serializer.DeserializeFromUtf8<T>(utf8)));
            }

            return results;
        }

        static decimal Check(int testCount, Action action)
        {
            var watch = Stopwatch.StartNew();

            for (int i = 0; i < testCount; i++)
            {
                action();
            }

            watch.Stop();

            return watch.ElapsedMilliseconds;
        }
    }

    interface IJsonParser
    {
        string Name { get; }
        string SerializeToString<T>(T instance);
        byte[] SerializeToUtf8<T>(T instance);


        T DeserializeFromString<T>(string json);
        T DeserializeFromUtf8<T>(byte[] utf8json);


    }
}
