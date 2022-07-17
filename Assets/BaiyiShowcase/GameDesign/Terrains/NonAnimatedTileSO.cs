using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace BaiyiShowcase.GameDesign.Terrains
{
    [CreateAssetMenu(fileName = "NonAnimatedTileSO", menuName = "SO/Terrain/NonAnimatedTileSO")]
    public class NonAnimatedTileSO : TileSO
    {
        [PreviewField(100f, ObjectFieldAlignment.Center)]
        public Tile[] tiles;
    }
}