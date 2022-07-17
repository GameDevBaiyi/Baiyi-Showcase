using System;
using System.Collections.Generic;
using BaiyiShowcase.GameDesign;
using BaiyiShowcase.GameDesign.Terrains;
using BaiyiUtilities;
using BaiyiUtilities.Singleton;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BaiyiShowcase.NewGame
{
    public class NewGameSettings : Singleton<NewGameSettings>
    {
        [Required]
        [SerializeField] private GameDesignSO _gameDesignSO;

        public string seed;
        public int mapSize;
        public TerrainSO terrainSO;
        public int colonistsCount;

        public NewGameColonist[] colonists;


        [Button]
        public void Initialize()
        {
            seed = Assistant.GetRandomString(_gameDesignSO.groundGenerationDesign.seedLength);
            mapSize = _gameDesignSO.groundGenerationDesign.mapSizeDefault;
            //TerrainSO暂时就一个.不管了.
            colonistsCount = _gameDesignSO.newGameSettingsDesign.characterCountDefault;
        }

        [Button]
        public void InitializeColonistsData()
        {
            colonists = new NewGameColonist[colonistsCount];
            for (int i = 0; i < colonistsCount; i++)
            {
                colonists[i] = new NewGameColonist();
            }

            foreach (NewGameColonist newGameColonist in colonists)
            {
                newGameColonist.colonistName = _gameDesignSO.newGameSettingsDesign.namesToRandomize.GetRandomElement();

                newGameColonist.sprites = new List<Sprite>();
                foreach (ColonistsCustomization colonistsDesignCustomization in _gameDesignSO.colonistsDesign
                             .customizations)
                {
                    newGameColonist.sprites.Add(colonistsDesignCustomization.sprites.GetRandomElement());
                }

                newGameColonist.skills = new Dictionary<string, int>();

                Vector2Int skillValueRange = _gameDesignSO.newGameSettingsDesign.skillValueRange;
                foreach (string skill in _gameDesignSO.newGameSettingsDesign.skills)
                {
                    newGameColonist.skills[skill] = skillValueRange.GetRandomValueInRange();
                }
            }
        }

        [Button]
        public void LoadBaseMapScene()
        {
            SceneManager.LoadScene(_gameDesignSO.commonDesign.baseMapSceneIndex);
        }
    }

    [Serializable]
    public class NewGameColonist
    {
        public string colonistName;
        public List<Sprite> sprites;
        public Dictionary<string, int> skills;
    }
}