using System.Collections.Generic;
using System.Linq;
using BaiyiShowcase.GameDesign;
using BaiyiShowcase.NewGame;
using BaiyiUtilities;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BaiyiShowcase.Colonists
{
    public class ColonistInitializer : MonoBehaviour
    {
        [Required]
        [SerializeField] private GameDesignSO _gameDesignSO;
        [Required]
        [SerializeField] private Colonist _colonist;
        
        public ColonistSaveDataHandler saveDataHandler;

        public void InitializeColonist(NewGameColonist newGameColonist)
        {
            //初始化外观
            IEnumerable<string> bodyPartNames = _gameDesignSO.colonistsDesign.customizations.Select(t => t.name);
            Transform colonistTransform = _colonist.transform;
            int index = 0;
            foreach (string bodyPartName in bodyPartNames)
            {
                if (colonistTransform.FindInAllChildren(bodyPartName)
                    .TryGetComponent(out SpriteRenderer spriteRenderer))
                {
                    spriteRenderer.sprite = newGameColonist.sprites[index];
                }
                else
                {
                    Debug.Log("没有找到名为 " + bodyPartName + " 上的Sprite Renderer");
                }

                index++;
            }

            //角色名.
            _colonist.colonistName = newGameColonist.colonistName;
        }
    }
}