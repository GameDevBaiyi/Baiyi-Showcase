using BaiyiShowcase.OrderSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BaiyiShowcase.Ores
{
    public class Ore : MonoBehaviour
    {
        [Required]
        public SpriteRenderer spriteRenderer;
        [Required]
        public OreSaveDataHandler oreSaveDataHandler; 

        public OreSO oreSO;
        
        private OrderType _orderType;
        [ShowInInspector]
        public OrderType OrderTypeProperty
        {
            get => _orderType;
            set
            {
                OrderCollections.changingOrderCollection?.Invoke(_orderType, value, this.transform);
                _orderType = value;

                //简单的显示Icon. 可以做出更复杂的随着UI的Sprite变动而变化的Icon. 此处只是为了观察, 未实现系统的Icon.
                if (_orderType == OrderType.Nothing)
                {
                    _icon.SetActive(false);
                }
                else
                {
                    _icon.SetActive(true);
                }
            }
        }

        [Required]
        [SerializeField] private GameObject _icon;
    }
}