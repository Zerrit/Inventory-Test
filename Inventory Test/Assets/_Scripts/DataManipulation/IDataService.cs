using System.Collections.Generic;
using _Scripts.InventorySystem.Model;

namespace _Scripts.DataManipulation
{
    public interface IDataService
    {
        void SaveData(List<Item> items);
        bool TryLoadData(out List<Item> items);
    }
}