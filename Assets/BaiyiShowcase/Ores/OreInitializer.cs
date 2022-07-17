using BaiyiUtilities;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BaiyiShowcase.Ores
{
    public class OreInitializer : MonoBehaviour
    {
        [Required]
        [SerializeField] private Ore _ore;
        [Required]
        public OreSaveDataHandler oreSaveDataHandler;

        public void InitializeOre(OreSO oreSO)
        {
            _ore.oreSO = oreSO;
            _ore.spriteRenderer.sprite = oreSO.sprites.GetRandomElement();
        }
    }
}