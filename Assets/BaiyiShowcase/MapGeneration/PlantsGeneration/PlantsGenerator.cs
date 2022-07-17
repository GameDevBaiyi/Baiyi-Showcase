using BaiyiShowcase.GameDesign;
using BaiyiShowcase.NewGame;
using BaiyiShowcase.Plants.Crops;
using BaiyiShowcase.Plants.Trees;
using BaiyiShowcase.SaveLoadSystem.LoadManage;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BaiyiShowcase.MapGeneration.PlantsGeneration
{
    public class PlantsGenerator : MonoBehaviour
    {
        [Required]
        [SerializeField] private GameDesignSO _gameDesignSO;
        [Required]
        [SerializeField] private Plants _plants;
        [Required]
        [SerializeField] private Transform _plantsParent;

        private bool IsNewScene => LoadManager.Instance.IsNewScene;
        
        private int Seed
        {
            get
            {
                if (NewGameSettings.Instance == true)
                {
                    return NewGameSettings.Instance.seed.GetHashCode();
                }
                else
                {
                    Debug.Log("未找到NewGameSettings.Instance.seed , 使用随机Seed");
                    return Random.Range(-1000, 1000);
                }
            }
        }

        private void Awake()
        {
            PlantsInitializer.OnEndInitializingPlants += GeneratePlants;
        }

        private void OnDestroy()
        {
            PlantsInitializer.OnEndInitializingPlants -= GeneratePlants;
        }

        private void GeneratePlants()
        {
            if (IsNewScene)
            {
                FirstlyGeneratePlants();
            }
            else
            {
                RestoreEverySinglePlantData();
            }
        }

        [Button]
        private void FirstlyGeneratePlants()
        {
            Random.InitState(Seed);
            foreach (InitialPlantData initialPlantData in _plants.initialPlantDataList)
            {
                if (initialPlantData.plantSO is TreeSO treeSO)
                {
                    TreeInitializer treeInitializer = Instantiate(_gameDesignSO.plantsDesign.treePrefab, initialPlantData.position,
                        Quaternion.identity,
                        _plantsParent);
                    treeInitializer.InitializeTree(treeSO);
                }
                else if (initialPlantData.plantSO is CropSO cropSO)
                {
                    CropInitializer cropInitializer = Instantiate(_gameDesignSO.plantsDesign.cropPrefab, initialPlantData.position,
                        Quaternion.identity,
                        _plantsParent);
                    cropInitializer.InitializeCrop(cropSO);
                }
            }
        }

        [Button]
        private void RestoreEverySinglePlantData()
        {
            foreach (TreeSaveData treeSaveData in _plants.treeSaveDataList)
            {
                TreeInitializer treeInitializer = Instantiate(_gameDesignSO.plantsDesign.treePrefab, _plantsParent);
                treeInitializer.saveDataHandler.RestoreTreeSaveData(treeSaveData);
            }

            foreach (CropSaveData cropSaveData in _plants.cropSaveDataList)
            {
                CropInitializer cropInitializer = Instantiate(_gameDesignSO.plantsDesign.cropPrefab, _plantsParent);
                cropInitializer.saveDataHandler.RestoreCropSaveData(cropSaveData);
            }
        }


        [Button]
        public void DestroyImmediatePlants()
        {
            while (true)
            {
                if (_plantsParent.childCount == 0) break;

                DestroyImmediate(_plantsParent.GetChild(0).gameObject);
            }
        }
    }
}