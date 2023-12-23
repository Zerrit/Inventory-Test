using _Scripts.InventorySystem.View;
using _Scripts.StaticData;
using UnityEngine;

namespace _Scripts.InventorySystem.Factory
{
    public interface IItemFactory
    {
        public ItemView GetRandomItemView(Transform container);
        public ItemView GetItemView(ItemData data, Transform container);
    }
}