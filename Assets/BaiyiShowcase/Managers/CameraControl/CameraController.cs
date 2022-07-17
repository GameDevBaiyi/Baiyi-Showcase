using BaiyiShowcase.GameDesign;
using BaiyiShowcase.MapGeneration.ColonistsGeneration;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BaiyiShowcase.Managers.CameraControl
{
    public class CameraController : BaiyiUtilities.Singleton.Singleton<CameraController>
    {
        [Required]
        [SerializeField] private GameDesignSO _gameDesignSO;
        [Required]
        [SerializeField] private Camera _camera;
        [Required]
        [SerializeField] private Transform _colonists;

        private Transform _transform;
        private Vector2 _inputValue;

        protected override void Awake()
        {
            base.Awake();
            if (Instance != this) return;

            _transform = this.transform;
            _camera = this.GetComponent<Camera>();
            ColonistsGenerator.OnEndGeneratingColonists += MoveCameraToCharacter;
        }

        private void OnDestroy()
        {
            ColonistsGenerator.OnEndGeneratingColonists -= MoveCameraToCharacter;
        }

        private void MoveCameraToCharacter()
        {
            foreach (Transform colonist in _colonists)
            {
                if (colonist == true)
                {
                    this._transform.position = colonist.position + new Vector3(0f, 0f, -10f);
                }
            }
        }

        private void LateUpdate()
        {
            _inputValue = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            if (_inputValue != Vector2.zero)
            {
                MoveCamera(_inputValue.normalized);
            }

            if (Input.mouseScrollDelta.y != 0)
            {
                float currentOrthographicSize = _camera.orthographicSize;
                float targetOrthographicSize = Mathf.Clamp(currentOrthographicSize - Input.mouseScrollDelta.y,
                    _gameDesignSO.commonDesign.cameraOrthographicSizeRange.x,
                    _gameDesignSO.commonDesign.cameraOrthographicSizeRange.y);
                _camera.orthographicSize = targetOrthographicSize;
            }
        }

        private void MoveCamera(Vector2 direction)
        {
            _transform.Translate(Time.deltaTime * _gameDesignSO.commonDesign.cameraSpeed *
                                 (_camera.orthographicSize / 10f) * direction);
        }
    }
}