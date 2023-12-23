using System;
using _Scripts.InventorySystem.Model;
using UnityEngine;

namespace _Scripts.InventorySystem.View
{
    public class GridCell : MonoBehaviour
    {
        public event Action<ItemView> OnRemoveItem;
        public event Action<ItemView, GridCell> OnDropItem;

        [field:SerializeField] public RectTransform RectTransform { get; private set; }
        public Vector2Int CellPosition { get; private set; }


        public void Initialize(Vector2Int cellPosition)
        {
            CellPosition = cellPosition;
        }


        public void RemoveItem(ItemView itemView)
        {
            OnRemoveItem?.Invoke(itemView);
        }

        public void DropItem(ItemView itemView)
        {
            OnDropItem?.Invoke(itemView, this);
        }
    }
}