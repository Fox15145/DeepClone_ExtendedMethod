using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Fox15145.Sample.ExtendedMethod.DeepClone
{
    /// <summary>
    /// https://stackoverflow.com/questions/129389/how-do-you-do-a-deep-copy-of-an-object-in-net
    /// Notes :
    /// Your class MUST be marked as [Serializable] for this to work.
    /// Your source file must include the following code:
    /// </summary>
    [Serializable]
    class Program
    {
        static void Main(string[] args)
        {
            IReadOnlyDictionary<int, string> Tiroirs = new Dictionary<int, string>();

            (Tiroirs as Dictionary<int, string>).Add(1, "Tiroir 1");
            (Tiroirs as Dictionary<int, string>).Add(2, "Tiroir 2");
            (Tiroirs as Dictionary<int, string>).Add(3, "Tiroir 3");

            IReadOnlyDictionary<int, string> Tiroirs2 = Tiroirs.DeepClone();

            (Tiroirs as Dictionary<int, string>)[1] = "le 1er";
            Console.WriteLine($"Tiroirs[1] = {Tiroirs[1]}");
            Console.WriteLine($"Tiroirs2[1] = {Tiroirs2[1]}");
        }


    }
    /// <summary>
    /// using System.Runtime.Serialization.Formatters.Binary;
    /// using System.IO;
    /// </summary>
    static class ExtendedMehod
    {
        public static T DeepClone<T>(this T obj)
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                ms.Position = 0;

                return (T)formatter.Deserialize(ms);
            }
        }
    }
}
