using System;
using BaiyiShowcase.GameDesign;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BaiyiShowcase.Ores
{
    public class OreSaveDataHandler : MonoBehaviour
    {
        [Required]
        [SerializeField] private GameDesignSO _gameDesignSO;
        [Required]
        [SerializeField] private Transform _transform;
        [Required]
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [Required]
        [SerializeField] private Ore _ore;

        public OreSaveData CollectOreData()
        {
            return new OreSaveData()
            {
                position = _transform.position,
                oreSOIndex = Array.IndexOf(_gameDesignSO.oresDesign.oreSOs, _ore.oreSO),
                spriteIndex = Array.IndexOf(_ore.oreSO.sprites, _spriteRenderer.sprite)
            };
        }

        public void RestoreOreData(OreSaveData oreSaveData)
        {
            _ore.oreSO = _gameDesignSO.oresDesign.oreSOs[oreSaveData.oreSOIndex];
            _spriteRenderer.sprite = _ore.oreSO.sprites[oreSaveData.spriteIndex];
        }
    }
}