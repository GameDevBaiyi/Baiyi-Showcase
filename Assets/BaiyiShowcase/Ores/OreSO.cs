using Sirenix.OdinInspector;
using UnityEngine;

namespace BaiyiShowcase.Ores
{
    [CreateAssetMenu(fileName = "OreSO", menuName = "SO/OreSO")]
    public class OreSO : ScriptableObject
    {
        public string oreName;
        [TextArea(5, 10)]
        public string description;
        [PreviewField(ObjectFieldAlignment.Center)] [ListDrawerSettings(Expanded = true)]
        public Sprite[] sprites;
    }
}