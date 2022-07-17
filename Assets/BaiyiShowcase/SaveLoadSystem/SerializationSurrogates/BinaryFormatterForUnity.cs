using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace BaiyiShowcase.SaveLoadSystem.SerializationSurrogates
{
    public static class BinaryFormatterForUnity
    {
        public static BinaryFormatter GetBinaryFormatter()
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            SurrogateSelector selector = new SurrogateSelector();

            Vector3Surrogate vector3Surrogate = new Vector3Surrogate();
            QuaternionSurrogate quaternionSurrogate = new QuaternionSurrogate();
            Vector2IntSurrogate vector2IntSurrogate = new Vector2IntSurrogate();
            Vector2Surrogate vector2Surrogate = new Vector2Surrogate();

            selector.AddSurrogate(typeof(Vector3), new StreamingContext(StreamingContextStates.All), vector3Surrogate);
            selector.AddSurrogate(typeof(Quaternion), new StreamingContext(StreamingContextStates.All),
                quaternionSurrogate);
            selector.AddSurrogate(typeof(Vector2Int), new StreamingContext(StreamingContextStates.All),
                vector2IntSurrogate);
            selector.AddSurrogate(typeof(Vector2), new StreamingContext(StreamingContextStates.All),
                vector2Surrogate);
            
            binaryFormatter.SurrogateSelector = selector;

            return binaryFormatter;
        }
    }
}