using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BaiyiShowcase.GameDesign
{
    [Serializable]
    public class ColonistsCustomization
    {
        public string name;

        [PreviewField(100f, ObjectFieldAlignment.Center )]
        public Sprite[] sprites;
    }
}