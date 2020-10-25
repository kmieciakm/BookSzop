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
        public static T DeepClone<T>(this T obj)
        {
            if (!obj.GetType().GetCustomAttributes(false).Contains((typeof(SerializableAttribute))))
            {
                throw new NotSupportedException(
                    $"Class {typeof(T)} does not contain required attribute {nameof(SerializableAttribute)}");
            }

            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                ms.Position = 0;

                return (T)formatter.Deserialize(ms);
            }
        }

        public static ICollection<T> DeepCloneCollection<T>(this ICollection<T> collection)
        {
            List<T> newCollection = new List<T>(0);
            foreach (var element in collection)
            {
                newCollection.Add(DeepClone<T>(element));
            }
            return newCollection;
        }
    }
}
