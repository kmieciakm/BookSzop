using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace DatabaseManager.Utils
{
    public static class ModelsUtils
    {
        public static T DeepClone<T>(T obj)
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                ms.Position = 0;

                return (T)formatter.Deserialize(ms);
            }
        }

        public static ICollection<T> DeepCloneCollection<T>(ICollection<T> collection)
        {
            List<T> newCollection = new List<T>();
            foreach (var element in collection)
            {
                newCollection.Add(DeepClone<T>(element));
            }
            return newCollection;
        }
    }
}
