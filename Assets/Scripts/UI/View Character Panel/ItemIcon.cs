using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemIcon : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] private Image _itemImage;
    private RectTransform _rectTransform;

    private CharacterItemSo _item;
    private ItemsPanelBehaviour _parentPanel;
    private ItemOnSceneHolder _itemHolder;

    private float _dragThreshold = 5;
    private bool _hasDrag;

    public void ShowItem(CharacterItemSo item, ItemsPanelBehaviour parentPanel)
    {
        _item = item;
        _parentPanel = parentPanel;
        _itemImage.sprite = _item.ItemSprite;
        _rectTransform = GetComponent<RectTransform>();
        _itemHolder = ServiceLocator.Instance.ItemOnSceneHolder;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!_hasDrag)
        {
            if (eventData.delta.magnitude > _dragThreshold)
                _hasDrag = true;
        }
        else
        {
            _rectTransform.position = eventData.position;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!_hasDrag)
        {
            OnIconClick();
        }   
        else
        {
            //вернуть на место или типа того
        }
    }

    private void OnIconClick()
    {
        _itemHolder.ShowItem(_item.ItemPrefab);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }
}