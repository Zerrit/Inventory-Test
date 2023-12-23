using System;
using _Scripts.StaticData;
using UnityEngine;

namespace _Scripts.InventorySystem.Model
{
    [Serializable]
    public struct Item
    {
        public ItemData itemData;
        public Vector2Int position;

        public Item(ItemData itemData, Vector2Int position)
        {
            this.itemData = itemData;
            this.position = position;
        }
    }
}