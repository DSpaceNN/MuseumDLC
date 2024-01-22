using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemIcon : MonoBehaviour, IDragHandler, IBeginDragHandler, IPointerUpHandler, IPointerDownHandler, IInitializePotentialDragHandler, IEndDragHandler
{
    public static event Action OnBeginDragIcon;
    public static event Action OnEndDragIcon;
    public static event Action<CharacterItemSo> OnDragItemOnCharacterIcon;

    [SerializeField] private Image _itemIconImage;
    [SerializeField] private Image _itemDragImage;

    [SerializeField] private Image _infoImage;
    [SerializeField] private Image _blockImage;
    [SerializeField] private Image _equipedImage;
    
    private RectTransform _dragRectTransform;
    private Vector3 _startRectDragPosition;

    private CharacterItemSo _item;
    private ItemsPanelBehaviour _parentPanel;
    private ItemOnSceneHolder _itemHolder;
    private CanvasController _canvasController;

    private bool _canEquip;
    private float _dragThreshold = 3;
    private bool _hasDrag;
    private bool _dragOnCharacter;

    private EventSystem _eventSystem;
    private GraphicRaycaster _raycaster;

    public void ShowItem(CharacterItemSo item, ItemsPanelBehaviour parentPanel, CanvasController canvasController)
    {
        _eventSystem = EventSystem.current;
        _raycaster = GetComponent<GraphicRaycaster>();

        _item = item;
        _parentPanel = parentPanel;
        _canvasController = canvasController;

        _itemIconImage.sprite = _item.ItemSprite;
        _itemDragImage.sprite = _item.ItemSprite;

        _dragRectTransform = _itemDragImage.GetComponent<RectTransform>();
        _startRectDragPosition = _dragRectTransform.position;

        _itemHolder = ServiceLocator.Instance.ItemOnSceneHolder;

        _canEquip = ServiceLocator.Instance.CharacterDresser.CanEquipItem(_item);
        ServiceLocator.Instance.CharacterDresser.OnItemEquiped += OnItemEquiped;

        _equipedImage.gameObject.SetActive(false);
        _blockImage.gameObject.SetActive(!_canEquip);
    }

    private void OnItemEquiped(CharacterItemSo itemSo)
    {
        _canEquip = ServiceLocator.Instance.CharacterDresser.CanEquipItem(_item);
        _blockImage.gameObject.SetActive(!_canEquip);

        if(itemSo == _item)
            _equipedImage.gameObject.SetActive(true);
    }

    public void OnInitializePotentialDrag(PointerEventData eventData)
    {
        eventData.useDragThreshold = true;
        eventData.tangentialPressure = _dragThreshold;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {   
        OnBeginDragIcon?.Invoke();
        _hasDrag = true;
        _itemDragImage.transform.SetParent(_canvasController.gameObject.transform);
        _itemDragImage.transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        _dragRectTransform.position = eventData.position;
        RaycastToCanvas();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        OnEndDrag();
        OnEndDragIcon?.Invoke();
        _itemDragImage.transform.SetParent(this.transform);
        _dragRectTransform.position = _startRectDragPosition;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!_hasDrag)
            OnIconClick();
        _hasDrag = false;
    }

    private void OnIconClick()
    {
        _itemHolder.ShowItem(_item.ItemPrefab);
    }

    public void OnPointerDown(PointerEventData eventData) { }

    private void RaycastToCanvas()
    {
        List<RaycastResult> results = new List<RaycastResult>();
        var pointerEventData = new PointerEventData(_eventSystem);
        pointerEventData.position = Input.mousePosition;
        _eventSystem.RaycastAll(pointerEventData, results);

        if (results.Count > 0)
        {
            if (results[0].gameObject.TryGetComponent(out MainCharacterRawImageBehaviour rawImageMb))
            {
                if (!_dragOnCharacter)
                {
                    _dragOnCharacter = true;
                    Debug.Log("навели на персонажа");
                }
            }
            else
            {
                if (_dragOnCharacter)
                {
                    _dragOnCharacter = false;
                    Debug.Log("убрали с персонажа");
                }
            }
        }
        else
        {
            if (_dragOnCharacter)
            {
                _dragOnCharacter = false;
                Debug.Log("убрали с персонажа");
            }
        }
    }

    private void OnEndDrag()
    {
        if (_dragOnCharacter)
            OnDragItemOnCharacterIcon?.Invoke(_item);
    }
}