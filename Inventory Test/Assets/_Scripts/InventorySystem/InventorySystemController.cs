using System;
using System.Collections.Generic;
using _Scripts.DataManipulation;
using _Scripts.InventorySystem.Model;
using _Scripts.StaticData;
using UnityEngine;

namespace _Scripts.InventorySystem
{
    public class InventorySystemController
    {
        public event Action<Item> OnItemAdded;
        //public event Action OnItemRemoved;
        
        private readonly InventorySystemModel _model;
        private readonly IDataService _dataService;


        public InventorySystemController()
        {
            _model = new InventorySystemModel();
            _dataService = new DataService();
            
            Debug.Log("Контроллер инициализирована");
        }

        public void OpenInventory()
        {
            if (_dataService.TryLoadData(out List<Item> loadedItems))
            {
                foreach (Item item in loadedItems)
                {
                    if(TryAddItem(item.itemData, item.position))
                        OnItemAdded?.Invoke(item);
                }
                Debug.Log("Данные инвентаря были загружены");
            }
        }
        public void CloseInventory() => _dataService.SaveData(_model.storagedItems);
        
        
        public bool CheckCells(ItemData itemData, Vector2Int targetCell)
        {
            if ((targetCell.x + itemData.Size.x > 10)||(targetCell.y + itemData.Size.y > 10)) return false;
            
            for (int i = targetCell.y; i < targetCell.y + itemData.Size.y; i++)
            {
                for (int j = targetCell.x; j < targetCell.x + itemData.Size.x; j++)
                {
                    if (_model.inventoryGrid[j, i]) return false;
                }
            }
            Debug.Log("Данное место не занято");
            return true;
        }
        
        public bool TryAddItem(ItemData itemData, Vector2Int targetCell)
        {
            if(!CheckCells(itemData, targetCell)) return false;
            
            for (int i = targetCell.y; i < targetCell.y + itemData.Size.y; i++)
            {
                for (int j = targetCell.x; j < targetCell.x + itemData.Size.x; j++)
                {
                    _model.inventoryGrid[j, i] = true;
                }
            }
            
            _model.storagedItems.Add(new Item(itemData, targetCell));
            Debug.Log("Предмет добавлен");
            return true;
        }

        public bool TryRemoveItem(ItemData itemData, Vector2Int targetCell)
        {
            Item removingItem = new Item(itemData, targetCell);
            if (_model.storagedItems.Contains(removingItem))
            {
                for (int i = removingItem.position.y; i < removingItem.position.y + removingItem.itemData.Size.y; i++)
                {
                    for (int j = removingItem.position.x; j < removingItem.position.x + removingItem.itemData.Size.x; j++)
                    {
                        _model.inventoryGrid[j, i] = false;
                    }
                }
                _model.storagedItems.Remove(removingItem);
                return true;
            }
            return false;
        }
    }
}