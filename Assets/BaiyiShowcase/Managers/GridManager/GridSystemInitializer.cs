using System;
using BaiyiShowcase.GameDesign;
using BaiyiShowcase.NewGame;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BaiyiShowcase.Managers.GridManager
{
    public class GridSystemInitializer : MonoBehaviour
    {
        [Required]
        [SerializeField] private GameDesignSO _gameDesignSO;
        [Required]
        [SerializeField] private GridSystem _gridSystem;

        public static event Action OnEndInitializingGridSystem;

        [Button]
        private void Reset()
        {
            _gridSystem = this.GetComponent<GridSystem>();
        }

        private void Start()
        {
            FirstlyInitializeGrid();

            OnEndInitializingGridSystem?.Invoke();
        }

        private void FirstlyInitializeGrid()
        {
            _gridSystem.currentMapSize =
                SceneManager.GetActiveScene().buildIndex == _gameDesignSO.commonDesign.baseMapSceneIndex
                    ? NewGameSettings.Instance.mapSize
                    : _gameDesignSO.commonDesign.wildMapSize;

            _gridSystem.nodeSize = _gameDesignSO.commonDesign.nodeSize;
        }
    }
}