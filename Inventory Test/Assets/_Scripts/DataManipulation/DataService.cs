using System;
using System.Collections.Generic;
using System.IO;
using _Scripts.InventorySystem.Model;
using UnityEngine;

namespace _Scripts.DataManipulation
{
    public class DataService : IDataService
    {
        private readonly string _defaultSavePath = Path.Combine(Application.persistentDataPath, "InventorySaveData.json");
        
        
        public void SaveData(List<Item> items)
        {
            string json = JsonUtility.ToJson(new InventoryData(items));
            File.WriteAllText(_defaultSavePath, json);
            Debug.Log("Данные инвентаря были сохранены");
        }
        
        public bool TryLoadData(out List<Item> items)
        {
            if (!File.Exists(_defaultSavePath))
            {
                Debug.Log("Файл сохранений не найден");
                items = null;
                return false;
            }
            string loadedJson = File.ReadAllText(_defaultSavePath);
            items = JsonUtility.FromJson<InventoryData>(loadedJson).items;
            Debug.Log("Сохранения загружены");
            return true;
        }
    }
}
