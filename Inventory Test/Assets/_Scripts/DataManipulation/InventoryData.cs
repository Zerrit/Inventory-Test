using System;
using System.Collections.Generic;
using _Scripts.InventorySystem.Model;

namespace _Scripts.DataManipulation
{
    [Serializable]
    public class InventoryData
    {
        public List<Item> items;
        
        public InventoryData(List<Item> item)
        {
            items = item;
        }
    }
}