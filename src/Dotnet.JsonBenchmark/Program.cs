﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Npgg;

namespace Dotnet.JsonBenchmark
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Benchmark(new FlatSample() { a = "aaa", b = "bbbb", c = "ccccc" }, 500000);

            Benchmark(new { a = new { a = new { a = new { a = new { a = "aaa", b = "bbbb", c = "ccccc" } } } } }, 500000);


        }

        static void Benchmark<T>(T instance, int testCount)
        {
            var serializers = new Type[]
            {
                typeof(NewtonsoftJsonParser),
                typeof(SystemTextJsonParser),
                typeof(JilJsonParser),
                typeof(Utf8JsonParser),
            }
                .Where(type => type.IsInterface == false && typeof(IJsonParser).IsAssignableFrom(type))
                .Select(type => (IJsonParser)Activator.CreateInstance(type));

            Dictionary<string, List<ConsoleBarChartOption>> results = new();

            results.Add(nameof(IJsonParser.SerializeToString), new());
            results.Add(nameof(IJsonParser.DeserializeFromString), new());
            results.Add(nameof(IJsonParser.SerializeToUtf8), new());
            results.Add(nameof(IJsonParser.DeserializeFromUtf8), new());

            foreach (var serializer in serializers)
            {
                var json = serializer.SerializeToString(instance);

                results[nameof(IJsonParser.SerializeToString)].Add( serializer.Check(testCount, () => serializer.SerializeToString(instance)));
                results[nameof(IJsonParser.DeserializeFromString)].Add(serializer.Check(testCount, () => serializer.DeserializeFromString<T>(json)));


                var utf8 = serializer.SerializeToUtf8(instance);

                results[nameof(IJsonParser.SerializeToUtf8)].Add(serializer.Check(testCount, () => serializer.SerializeToUtf8(instance)));
                results[nameof(IJsonParser.DeserializeFromUtf8)].Add(serializer.Check(testCount, () => serializer.DeserializeFromUtf8<T>(utf8)));
            }

            foreach(var test in results)
            {
                Console.WriteLine(test.Key);
                Npgg.ConsoleBarChart.Write(test.Value);
            }
        }

    }

    public class FlatSample
    {
        public string a { get; set; }
        public string b { get; set; }
        public string c { get; set; }
    }
}
