using System.Collections.Generic;
using _Scripts.InventorySystem.Model;
using UnityEngine;

namespace _Scripts.StaticData
{
    [CreateAssetMenu(fileName = "AllItemData", menuName = "Items/All Items Data")]
    public class AllItemsData : ScriptableObject
    {
        [field:SerializeField] public List<ItemData> ItemsData { get; private set; }
    }
}