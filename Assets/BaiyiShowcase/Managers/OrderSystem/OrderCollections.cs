using System;
using System.Collections.Generic;
using BaiyiUtilities.Singleton;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BaiyiShowcase.OrderSystem
{
    public class OrderCollections : Singleton<OrderCollections>
    {
        public Dictionary<OrderType, List<Transform>> orderTypeDictionary =
            new Dictionary<OrderType, List<Transform>>();

        public static Action<OrderType, OrderType, Transform> changingOrderCollection;

        protected override void Awake()
        {
            base.Awake();

            if (Instance != this) return;
            changingOrderCollection += UpdateDictionary;

            Initialize();

            void Initialize()
            {
                foreach (OrderType order in Enum.GetValues(typeof(OrderType)))
                {
                    if (order == OrderType.Nothing) continue;

                    orderTypeDictionary[order] = new List<Transform>();
                }
            }
        }

        private void OnDestroy()
        {
            changingOrderCollection -= UpdateDictionary;
        }

        public bool HasOrder(OrderType orderType)
        {
            foreach (Transform orderTarget in orderTypeDictionary[orderType])
            {
                if (orderTarget == null)
                {
                    orderTypeDictionary[orderType].Remove(orderTarget);
                }
                else
                {
                    return true;
                }
            }

            return false;
        }

        public void RemoveOrder(OrderType order, Transform item)
        {
            if (order == OrderType.Nothing) return;

            orderTypeDictionary[order].Remove(item);
        }

        public void AddOrder(OrderType order, Transform item)
        {
            if (order == OrderType.Nothing) return;
            if(orderTypeDictionary[order].Contains(item)) return;

            orderTypeDictionary[order].Add(item);
        }

        private void UpdateDictionary(OrderType previousOrder, OrderType targetOrder, Transform markedIem)
        {
            RemoveOrder(previousOrder, markedIem);
            AddOrder(targetOrder, markedIem);
        }

        [Button]
        private void PrintDictionaryInfo(OrderType orderType)
        {
            Debug.Log(orderType.ToString() + " 类型的指令目前有 " + orderTypeDictionary[orderType].Count + " 个");
        }

        public bool TryLookUpOrder(OrderType orderType, out Transform target)
        {
            foreach (Transform orderTarget in orderTypeDictionary[orderType])
            {
                if (orderTarget == true)
                {
                    target = orderTarget;
                    return true;
                }
                else
                {
                    RemoveOrder(orderType, orderTarget);
                }
            }

            target = null;
            return false;
        }
    }
}