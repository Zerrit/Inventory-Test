using _Scripts.StaticData;
using UnityEngine;

namespace _Scripts.InventorySystem.View
{
    public interface IItemView
    {
        public GridCell ActualCell { get; }
        public ItemData Data { get; }
        
        public void Initialize(ItemData item);
        public void ChangeCell(GridCell cell);
        public void PutInPlace();
        public void ChangeContainer(Transform container);
    }
}