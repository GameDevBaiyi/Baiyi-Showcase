using System.Runtime.Serialization;
using UnityEngine;

namespace BaiyiShowcase.SaveLoadSystem.SerializationSurrogates
{
    public class Vector2IntSurrogate: ISerializationSurrogate
    {
        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
        {
            Vector2Int vector2Int = (Vector2Int)obj;
            info.AddValue("x", vector2Int.x);
            info.AddValue("y", vector2Int.y);
        }

        public object SetObjectData(object obj, SerializationInfo info, StreamingContext context,
            ISurrogateSelector selector)
        {
            Vector2Int vector2Int = (Vector2Int)obj;
            vector2Int.x = (int)info.GetValue("x", typeof(int));
            vector2Int.y = (int)info.GetValue("y", typeof(int));
         
            return vector2Int;
        }
    }
}