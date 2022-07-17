using Sirenix.OdinInspector;
using UnityEngine;

namespace BaiyiShowcase.Animals
{
    [CreateAssetMenu(fileName = "AnimalSO", menuName = "SO/AnimalSO")]
    public class AnimalSO : ScriptableObject
    {
        public AnimalInitializer animalPrefab;

        [Header("动物行为参数")]
        public Vector2 maxIdleTimeRange;
        public float moveRadius;
        public Vector2 maxWalkTimeRange;
        public Vector2 maxEatTimeRange;
        public Vector2 maxSleepTimeRange;
        public float moveSpeed = 3f;

        [Button]
        private void SetDefaultValue()
        {
            maxIdleTimeRange = new Vector2(5f, 10f);
            moveRadius = Random.Range(3f, 6f);
            maxWalkTimeRange = new Vector2(5f, 10f);
            maxEatTimeRange = new Vector2(5f, 10f);
            moveSpeed = Random.Range(0.2f, 0.4f);
        }
    }
}