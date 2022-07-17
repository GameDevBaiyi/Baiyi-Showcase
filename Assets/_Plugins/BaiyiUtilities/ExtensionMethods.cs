using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

// ReSharper disable PossibleMultipleEnumeration

namespace BaiyiUtilities
{
    public static class ExtensionMethods
    {
        public static T GetRandomElement<T>(this IEnumerable<T> collection)
        {
            return collection.ElementAt(Random.Range(0, collection.Count()));
        }

        public static T MaxBy<T>(this IEnumerable<T> collection, Func<T, IComparable> keySelector)
        {
            T max = collection.First();
            foreach (T item in collection)
            {
                if (keySelector(item).CompareTo(keySelector(max)) > 0)
                {
                    max = item;
                }
            }

            return max;
        }

        public static T MinBy<T>(this IEnumerable<T> collection, Func<T, IComparable> keySelector)
        {
            T min = collection.First();
            foreach (T item in collection)
            {
                if (keySelector(item).CompareTo(keySelector(min)) < 0)
                {
                    min = item;
                }
            }

            return min;
        }

        public static Transform FindInAllChildren(this Transform transform, string name)
        {
            Queue<Transform> transforms = new Queue<Transform>();
            transforms.Enqueue(transform);
            while (transforms.Count > 0)
            {
                Transform current = transforms.Dequeue();
                if (name == current.name) return current;
                foreach (Transform child in current)
                {
                    transforms.Enqueue(child);
                }
            }

            return null;
        }

        public static bool IsInRange(this int i, int min, int max)
        {
            return i <= max && i >= min;
        }

        public static bool IsInRange(this float f, float min, float max)
        {
            return f <= max && f >= min;
        }

        public static bool IsInRange(this Vector2Int vector2Int, int xMin, int xMax, int yMin, int yMax)
        {
            return vector2Int.x.IsInRange(xMin, xMax) && vector2Int.y.IsInRange(yMin, yMax);
        }

        public static string GetNameWithSubtractingFromSpace(this UnityEngine.Object o)
        {
            if (o == null) return null;

            string name = o.ToString();
            return name.Remove(name.IndexOf(' ', StringComparison.Ordinal));
        }

        public static IEnumerable<Vector2Int> Get4D(this Vector2Int startCoord)
        {
            return new List<Vector2Int>()
            {
                startCoord + Vector2Int.up,
                startCoord + Vector2Int.down,
                startCoord + Vector2Int.right,
                startCoord + Vector2Int.left
            };
        }

        public static IEnumerable<Vector2Int> Get8D(this Vector2Int startCoord)
        {
            List<Vector2Int> directions = new List<Vector2Int>();
            for (int x = -1; x < 2; x++)
            {
                for (int y = -1; y < 2; y++)
                {
                    if (x == 0 && y == 0) continue;

                    directions.Add(new Vector2Int(startCoord.x + x, startCoord.y + y));
                }
            }

            return directions;
        }

        public static int GetRandomValueInRange(this Vector2Int range)
        {
            return Random.Range(range.x, range.y);
        }

        public static float GetRandomValueInRange(this Vector2 range)
        {
            return Random.Range(range.x, range.y);
        }

        public static void DestroyAllChildren(this Transform parent)
        {
            List<Transform> children = new List<Transform>();
            foreach (Transform child in parent)
            {
                children.Add(child);
            }

            foreach (Transform child in children)
            {
                Object.Destroy(child.gameObject);
            }
        }
    }
}