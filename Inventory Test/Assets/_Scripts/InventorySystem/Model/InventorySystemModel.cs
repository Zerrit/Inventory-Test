using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.InventorySystem.Model
{
    public class InventorySystemModel
    {
        public List<Item> storagedItems;
        public bool[,] inventoryGrid;

        public InventorySystemModel()
        {
            storagedItems = new List<Item>();
            inventoryGrid = new bool[10, 10];
            
            Debug.Log("Модель инициализирована");
        }
    }
}