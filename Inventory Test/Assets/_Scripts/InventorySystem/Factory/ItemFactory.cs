using _Scripts.InventorySystem.View;
using _Scripts.StaticData;
using UnityEngine;

namespace _Scripts.InventorySystem.Factory
{
    public class ItemFactory : IItemFactory
    {
        private ItemView _itemViewPrefab;
        private AllItemsData _itemsData;
        
        public ItemFactory()
        {
            _itemViewPrefab = Resources.Load<ItemView>("ItemView");
            _itemsData = Resources.Load<AllItemsData>("Items/AllItemsData");
        }


        public ItemView GetRandomItemView(Transform container)
        {
            int i = Random.Range(0, _itemsData.ItemsData.Count);
            ItemView instance = Object.Instantiate(_itemViewPrefab, container);
            instance.Initialize(_itemsData.ItemsData[i]);
            return instance;
        }
        
        public ItemView GetItemView(ItemData data, Transform container)
        {
            ItemView instance = Object.Instantiate(_itemViewPrefab, container);
            instance.Initialize(data);
            return instance;
        }
    }
}
