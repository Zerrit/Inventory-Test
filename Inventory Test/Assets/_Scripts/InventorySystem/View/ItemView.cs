using _Scripts.StaticData;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _Scripts.InventorySystem.View
{
    public class ItemView : MonoBehaviour, IItemView, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
    {
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private Image itemImage;
        [SerializeField] private LayerMask inventoryLayer;
        [SerializeField] private LayerMask assortmentLayer;
        
        public GridCell ActualCell { get; private set; }
        public ItemData Data { get; private set; }


        public void Initialize(ItemData item)
        {
            Data = item;
            itemImage.sprite = Data.Icon;
            itemImage.rectTransform.sizeDelta = new Vector2(90 * Data.Size.x, 90* Data.Size.y);
        }
        
        
        public void ChangeCell(GridCell cell) 
        {
            ActualCell = cell;
            PutInPlace();
        }
        public void PutInPlace() => rectTransform.anchoredPosition = ActualCell.RectTransform.anchoredPosition;
        public void ChangeContainer(Transform container) => transform.SetParent(container, false);

        
        
        // DRAG METHOTDS
        public void OnBeginDrag(PointerEventData eventData)
        {
            rectTransform.SetAsLastSibling();
            itemImage.raycastTarget = false;
        }
        
        public void OnDrag(PointerEventData eventData)
        {
            //rectTransform.anchoredPosition += eventData.delta;
            rectTransform.position = Input.mousePosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            GridCell newCell = eventData.pointerCurrentRaycast.gameObject.GetComponent<GridCell>();

            if(!newCell) PutInPlace();
            else if(1 << newCell.gameObject.layer == inventoryLayer) newCell.DropItem(this);
            else if (1 << newCell.gameObject.layer == assortmentLayer)
            {
                if(1 << ActualCell.gameObject.layer == assortmentLayer) PutInPlace();
                else ActualCell.RemoveItem(this);
            }
            
            itemImage.raycastTarget = true;
        }

        // ОБРАБОТКА КЛИКА ПО ПРЕДМЕТУ
        public void OnPointerClick(PointerEventData eventData)
        {
            ActualCell.RemoveItem(this);
        }
    }
}