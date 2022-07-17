using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace BaiyiUtilities
{
    public static class Assistant
    {
        private static Camera _camera;
        public static Camera Camera
        {
            get
            {
                if (!_camera)
                {
                    _camera = Camera.main;
                    return _camera;
                }

                return _camera;
            }
        }

        public static int GetEnumCount<T>() where T : Enum
        {
            return Enum.GetValues(typeof(T)).Length;
        }

        public static string GetRandomString(int length)
        {
            char[] chars = new char[]
            {
                'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U',
                'V', 'W', 'X', 'Y', 'Z'
            };
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                builder.Append(chars.GetRandomElement());
            }

            return builder.ToString();
        }

        public static string GetCurrentFolderPath()
        {
            string path = "";

            foreach (Object obj in Selection.GetFiltered(typeof(Object), SelectionMode.Assets))
            {
                path = AssetDatabase.GetAssetPath(obj);
                if (!string.IsNullOrEmpty(path) && File.Exists(path))
                {
                    path = Path.GetDirectoryName(path);
                    break;
                }
            }

            return path;
        }

        public static void ClearConsoleLogs()
        {
            Assembly assembly = Assembly.GetAssembly(typeof(SceneView));
            Type logEntries = assembly.GetType("UnityEditor.LogEntries");
            MethodInfo clearConsoleMethod = logEntries.GetMethod("Clear");
            if (clearConsoleMethod != null) clearConsoleMethod.Invoke(new object(), null);
            Debug.Log("Cleared previous logs");
        }

        public static IEnumerable<Vector2Int> RandomFill(Vector2Int startCoord, Func<Vector2Int, bool> predicate,
            int fillSize = Int32.MaxValue)
        {
            List<Vector2Int> randomFill = new List<Vector2Int>();
            List<Vector2Int> openSet = new List<Vector2Int>();
            List<Vector2Int> closedSet = new List<Vector2Int>();

            if (predicate(startCoord))
            {
                openSet.Add(startCoord);
                closedSet.Add(startCoord);
            }
            else
            {
                Debug.Log("起始点不满足predicate");
                return randomFill;
            }

            while (randomFill.Count < fillSize)
            {
                if (openSet.Count == 0) break;

                Vector2Int current = openSet.GetRandomElement();
                openSet.Remove(current);
                randomFill.Add(current);

                foreach (Vector2Int surroundingCoord in current.Get4D())
                {
                    if (closedSet.Contains(surroundingCoord)) continue;

                    if (predicate(surroundingCoord))
                    {
                        openSet.Add(surroundingCoord);
                        closedSet.Add(surroundingCoord);
                    }
                    else
                    {
                        closedSet.Add(surroundingCoord);
                    }
                }

                if (openSet.Count == 0 || randomFill.Count >= fillSize) break;
            }

            return randomFill;
        }

        public static IEnumerable<Vector2Int> FloodFill4D(Vector2Int startCoord, Func<Vector2Int, bool> predicate,
            int floodFillSize = Int32.MaxValue)
        {
            List<Vector2Int> floodFill = new List<Vector2Int>();
            Queue<Vector2Int> openSet = new Queue<Vector2Int>();
            List<Vector2Int> closedList = new List<Vector2Int>();
            if (predicate(startCoord))
            {
                openSet.Enqueue(startCoord);
            }
            else
            {
                Debug.Log("初始点不满足predicate");
                return floodFill;
            }

            closedList.Add(startCoord);

            while (true)
            {
                Vector2Int current = openSet.Dequeue();
                floodFill.Add(current);

                foreach (Vector2Int cardinalDirection in current.Get4D())
                {
                    if (closedList.Contains(cardinalDirection)) continue;

                    if (predicate(cardinalDirection))
                    {
                        openSet.Enqueue(cardinalDirection);
                        closedList.Add(cardinalDirection);
                    }
                    else
                    {
                        closedList.Add(cardinalDirection);
                    }
                }


                if (openSet.Count == 0) break;
                if (floodFill.Count >= floodFillSize) break;
            }

            return floodFill;
        }

        public static IEnumerable<Vector2Int> FloodFill8D(Vector2Int startCoord, Func<Vector2Int, bool> predicate,
            int floodFillSize = Int32.MaxValue)
        {
            List<Vector2Int> floodFill = new List<Vector2Int>();
            Queue<Vector2Int> openSet = new Queue<Vector2Int>();
            List<Vector2Int> closedList = new List<Vector2Int>();
            if (predicate(startCoord))
            {
                openSet.Enqueue(startCoord);
            }
            else
            {
                Debug.Log("初始点不满足predicate");
                return floodFill;
            }

            closedList.Add(startCoord);

            while (true)
            {
                Vector2Int current = openSet.Dequeue();
                floodFill.Add(current);

                foreach (Vector2Int cardinalDirection in current.Get8D())
                {
                    if (closedList.Contains(cardinalDirection)) continue;

                    if (predicate(cardinalDirection))
                    {
                        openSet.Enqueue(cardinalDirection);
                        closedList.Add(cardinalDirection);
                    }
                    else
                    {
                        closedList.Add(cardinalDirection);
                    }
                }


                if (openSet.Count == 0) break;
                if (floodFill.Count >= floodFillSize) break;
            }

            return floodFill;
        }

        public static float[,] GetPerlinNoise(int mapSize, float frequency, int octaves = 4)
        {
            float[,] perlinNoise = new float[mapSize, mapSize];
            Vector2 offset = new Vector2(Random.Range(-10000f, 10000f), Random.Range(-10000f, 10000f));
            Vector2[] octaveOffsets = new Vector2[octaves];
            for (int i = 0; i < octaves; i++)
            {
                float offsetX = Random.Range(-10000f, 10000f) + offset.x;
                float offsetY = Random.Range(-10000f, 10000f) + offset.y;
                octaveOffsets[i] = new Vector2(offsetX, offsetY);
            }

            float startFrequency = frequency;

            float lacunarity = 2f;
            float persistence = 0.5f;

            float maxNoiseHeight = float.MinValue;
            float minNoiseHeight = float.MaxValue;

            for (int x = 0; x < mapSize; x++)
            {
                for (int y = 0; y < mapSize; y++)
                {
                    frequency = startFrequency;
                    float amplitude = 1f;

                    float sumOfNoise = 0f;

                    for (int i = 0; i < octaves; i++)
                    {
                        float sampleX = (float)x / mapSize * frequency + octaveOffsets[i].x;
                        float sampleY = (float)y / mapSize * frequency + octaveOffsets[i].y;

                        float octavesNoise = Mathf.PerlinNoise(sampleX, sampleY);
                        sumOfNoise += octavesNoise * amplitude;

                        frequency *= lacunarity;
                        amplitude *= persistence;
                    }

                    if (sumOfNoise > maxNoiseHeight)
                    {
                        maxNoiseHeight = sumOfNoise;
                    }
                    else if (sumOfNoise < minNoiseHeight)
                    {
                        minNoiseHeight = sumOfNoise;
                    }

                    perlinNoise[x, y] = sumOfNoise;
                }
            }

            for (int x = 0; x < mapSize; x++)
            {
                for (int y = 0; y < mapSize; y++)
                {
                    perlinNoise[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, perlinNoise[x, y]);
                }
            }

            return perlinNoise;
        }

        public static IEnumerable<T> GetAssetsByFolderPath<T>( string folderPath) where T : Object
        {
            string[] guids = AssetDatabase.FindAssets("t:" + typeof(T), new[] { folderPath });
            List<T> list = new();
            foreach (string guid in guids)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(guid);
                list.Add(AssetDatabase.LoadAssetAtPath<T>(assetPath));
            }

            return list;
        }
    }
}