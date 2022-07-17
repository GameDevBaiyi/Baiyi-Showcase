using BaiyiShowcase.GameDesign;
using BaiyiShowcase.Ores;
using BaiyiShowcase.SaveLoadSystem.LoadManage;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BaiyiShowcase.MapGeneration.OresGeneration
{
    public class OresGenerator : MonoBehaviour
    {
        [Required]
        [SerializeField] private GameDesignSO _gameDesignSO;
        [Required]
        [SerializeField] private Ores _ores;
        [Required]
        [SerializeField] private Transform _oresParent;
        private bool IsNewScene => LoadManager.Instance.IsNewScene;

        private void Awake()
        {
            OresInitializer.OnEndingInitializingOres += GenerateOres;
        }

        private void OnDestroy()
        {
            OresInitializer.OnEndingInitializingOres -= GenerateOres;
        }

        private void GenerateOres()
        {
            if (IsNewScene)
            {
                FirstlyGenerateOres();
            }
            else
            {
                RestoreEverySingleOreData();
            }
        }


        [Button]
        private void FirstlyGenerateOres()
        {
            foreach (InitialOreData initialOreData in _ores.initialOreDataList)
            {
                OreInitializer oreInitializer = Instantiate(_gameDesignSO.oresDesign.orePrefab,
                    initialOreData.position, Quaternion.identity,
                    _oresParent);
                oreInitializer.InitializeOre(initialOreData.oreSO);
            }
        }

        [Button]
        private void RestoreEverySingleOreData()
        {
            foreach (OreSaveData oreSaveData in _ores.oreSaveDataList)
            {
                OreInitializer oreInitializer = Instantiate(_gameDesignSO.oresDesign.orePrefab,
                    oreSaveData.position, Quaternion.identity, _oresParent);
                oreInitializer.oreSaveDataHandler.RestoreOreData(oreSaveData);
            }
        }

        [Button]
        private void DestroyImmediateOres()
        {
            while (true)
            {
                if (_oresParent.childCount == 0) break;

                DestroyImmediate(_oresParent.GetChild(0).gameObject);
            }
        }
    }
}