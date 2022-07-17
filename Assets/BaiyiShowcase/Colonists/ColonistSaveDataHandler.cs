using System;
using System.Collections.Generic;
using System.Linq;
using BaiyiShowcase.GameDesign;
using BaiyiUtilities;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BaiyiShowcase.Colonists
{
    public class ColonistSaveDataHandler : MonoBehaviour
    {
        [Required]
        [SerializeField] private GameDesignSO _gameDesignSO;
        [Required]
        [SerializeField] private Colonist _colonist;

        public ColonistSaveData CollectColonistData()
        {
            return new ColonistSaveData()
            {
                colonistName = _colonist.colonistName,
                position = _colonist.transform.position,
                spriteIndexes = _gameDesignSO.colonistsDesign.customizations.Select(t =>
                    Array.IndexOf(t.sprites,
                        _colonist.transform.FindInAllChildren(t.name).GetComponent<SpriteRenderer>().sprite)).ToArray()
            };
        }

        public void RestoreColonistData(ColonistSaveData colonistSaveData)
        {
            //恢复名字.
            _colonist.colonistName = colonistSaveData.colonistName;
            //恢复外观.
            int index = 0;
            Transform colonistTransform = _colonist.transform;
            foreach (ColonistsCustomization colonistsDesignCustomization in
                     _gameDesignSO.colonistsDesign.customizations)
            {
                if (colonistTransform.FindInAllChildren(colonistsDesignCustomization.name)
                    .TryGetComponent(out SpriteRenderer spriteRenderer))
                {
                    spriteRenderer.sprite = colonistsDesignCustomization.sprites[colonistSaveData.spriteIndexes[index]];
                }
                else
                {
                    Debug.Log("没有找到名为 " + colonistsDesignCustomization.name + " 上的Sprite Renderer");
                }

                index++;
            }
        }
    }
}