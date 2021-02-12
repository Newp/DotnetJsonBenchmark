using System;
using System.Diagnostics;
using Npgg;

namespace Dotnet.JsonBenchmark
{
    interface IJsonParser
    {
        string Name { get; }
        ConsoleColor BarColor { get; }
        string SerializeToString<T>(T instance);
        byte[] SerializeToUtf8<T>(T instance);


        T DeserializeFromString<T>(string json);
        T DeserializeFromUtf8<T>(byte[] utf8json);

        public ConsoleBarChartOption Check(int testCount, Action action)
        {
            var watch = Stopwatch.StartNew();

            for (int i = 0; i < testCount; i++)
            {
                action();
            }

            watch.Stop();

            var result = new ConsoleBarChartOption()
            {
                BarColor = this.BarColor,
                Name = this.Name,
                Value = watch.ElapsedMilliseconds
            };
            return result;
        }
    }
}
