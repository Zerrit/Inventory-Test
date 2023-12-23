using System.Collections.Generic;
using _Scripts.InventorySystem.Factory;
using _Scripts.InventorySystem.Model;
using UnityEngine;

namespace _Scripts.InventorySystem.View
{
    public class InventorySystemView : MonoBehaviour
    {
        [SerializeField] private GridCell cellPrefab;
        
        [SerializeField] private Transform inventoryCellsContainer;
        [SerializeField] private Transform assortmentCellsContainer;
        
        [SerializeField] private Transform inventoryItemsContainer;
        [SerializeField] private Transform assortmentItemsContainer;
        
        private GridCell[,] _inventoryGrid;
        private GridCell[,] _assortmentGrid;

        private List<IItemView> _assortmentItems;
        
        private int _freeAssortmentYIndex;
        private InventorySystemController _controller;
        private IItemFactory _factory;

        private const string inventoryLayer = "Inventory Cell";
        private const string assortmentLayer = "Assortment Cell";


        public void Initialize(InventorySystemController controller, IItemFactory factory)
        {
            _factory = factory;
            _controller = controller;
            _controller.OnItemAdded += DrowItem;
            
            CreateInventoryGrid(inventoryCellsContainer, new Vector2Int(10,10));
            CreateAssortmentGrid(assortmentCellsContainer, new Vector2Int(5, 10));
            Canvas.ForceUpdateCanvases();

            _assortmentItems = new List<IItemView>();
            FillRightGrid(4); // ЗАПОЛНЕНИЕ ПРАВОЙ ПАНЕЛИ
            
            Debug.Log("Представление инициализировано");
        }
        
        
        // ВОЗВРАТ В ПРАВУЮ ПАНЕЛЬ  
        private void ReturnToAssortment(IItemView item) 
        {
            if (!HasFreeSpace(item))
            {
                item.PutInPlace();
                return;
            }
            _controller.TryRemoveItem(item.Data, item.ActualCell.CellPosition);
            item.ChangeContainer(assortmentItemsContainer);
            _assortmentItems.Add(item);
            AddItemToAssortment(item);
        }
        
        // РАЗМЕЩЕНИЕ В НОВОЙ ПОЗИЦИИ
        private void PlaceItem(IItemView item, GridCell targetCell) 
        {
            if(!_controller.CheckCells(item.Data, targetCell.CellPosition)) item.PutInPlace();
            else 
            {
                TryRemoveAssortmentItem(item);
                _controller.TryRemoveItem(item.Data, item.ActualCell.CellPosition);
                _controller.TryAddItem(item.Data, targetCell.CellPosition);
                item.ChangeContainer(inventoryItemsContainer);
                item.ChangeCell(targetCell);
            }
        }
        
        // СОЗДАНИЕ И ОТРИСОВКА СОДЕРЖИМОГО ИНВЕНТАРЯ
        private void DrowItem(Item item) 
        {
            ItemView view = _factory.GetItemView(item.itemData, inventoryItemsContainer);
            view.ChangeCell( _inventoryGrid[item.position.x, item.position.y]);
            Debug.Log("Предмет Отрисован");
        }

        private void CreateInventoryGrid(Transform container, Vector2Int inventorySize)
        {
            _inventoryGrid = new GridCell[inventorySize.x, inventorySize.y];

            for (int i = 0; i < inventorySize.y; i++)
            {
                for (int j = 0; j < inventorySize.x; j++)
                {
                    _inventoryGrid[j, i] = Instantiate(cellPrefab, container);
                    _inventoryGrid[j, i].Initialize(new Vector2Int(j,i));
                    _inventoryGrid[j, i].gameObject.layer = LayerMask.NameToLayer(inventoryLayer);
                    _inventoryGrid[j, i].OnRemoveItem += ReturnToAssortment;
                    _inventoryGrid[j, i].OnDropItem += PlaceItem;
                }
            }
        }
        
        
        // RIGHT ASSORTMENT PANEL METHODS
        private void CreateAssortmentGrid(Transform container, Vector2Int inventorySize)
        {
            _assortmentGrid = new GridCell[inventorySize.x, inventorySize.y];

            for (int i = 0; i < inventorySize.y; i++)
            {
                for (int j = 0; j < inventorySize.x; j++)
                {
                    _assortmentGrid[j, i] = Instantiate(cellPrefab, container);
                    _assortmentGrid[j, i].Initialize(new Vector2Int(j,i));
                    _assortmentGrid[j, i].gameObject.layer = LayerMask.NameToLayer(assortmentLayer);
                }
            }
        }
        private void FillRightGrid(int itemCount)
        {
            for (int i = 0; i < itemCount; i++)
            {
                ItemView view = _factory.GetRandomItemView(assortmentItemsContainer);
                if (HasFreeSpace(view))
                {
                    _assortmentItems.Add(view);
                    AddItemToAssortment(view);
                }
                else
                {
                    Destroy(view.gameObject);
                    return;
                }
            }
        }
        private bool HasFreeSpace(IItemView item)
        {
            if (_freeAssortmentYIndex + item.Data.Size.y <= 10) return true;
            
            RecombineAssortment();
            return false;
        } 
        private void AddItemToAssortment(IItemView item)
        {
            item.ChangeCell(_assortmentGrid[0, _freeAssortmentYIndex]);
            _freeAssortmentYIndex += (int) item.Data.Size.y;
        }
        private void RecombineAssortment()
        {
            _freeAssortmentYIndex = 0;
            foreach (IItemView view in _assortmentItems)
            {
                AddItemToAssortment(view);    
            }
        }
        private void TryRemoveAssortmentItem(IItemView item)
        {
            if (_assortmentItems.Contains(item)) _assortmentItems.Remove(item);
        }
    }
}