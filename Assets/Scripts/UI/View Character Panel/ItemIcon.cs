using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemIcon : MonoBehaviour, IDragHandler, IBeginDragHandler, IInitializePotentialDragHandler, IEndDragHandler, IPointerClickHandler
{
    public static event Action OnBeginDragIcon;
    public static event Action OnEndDragIcon;

    public static event Action OnHoverEnterDragIcon;
    public static event Action OnHoverExitDragIcon;

    public static event Action<CharacterItemSo> OnDragItemOnCharacterIcon;
    public static event Action<CharacterItemSo> OnClickOnItem;

    [SerializeField] private Image _itemIconImage;
    [SerializeField] private Image _itemDragImage;
    [SerializeField] private Image _greenHaloDragImage;

    [SerializeField] private Image _infoImage;
    [SerializeField] private Image _blockImage;
    [SerializeField] private Image _equipedImage;
    [SerializeField] private Image _selectedImage;
    [SerializeField] private Image _selectedFrameImage;

    private Color _fullColor = Color.white;
    private Color _blockedColor = Color.white;

    private RectTransform _dragRectTransform;

    private CharacterItemSo _item;
    private ItemsPanelBehaviour _parentPanel;
    private ItemOnSceneHolder _itemHolder;
    private CanvasController _canvasController;

    private bool _canEquip;
    private bool _isEquipped;

    private float _dragThreshold = 3;
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

        _dragRectTransform = _greenHaloDragImage.GetComponent<RectTransform>();
        _greenHaloDragImage.gameObject.SetActive(false);

        _itemHolder = ServiceLocator.Instance.ItemOnSceneHolder;

        _canEquip = ServiceLocator.Instance.CharacterDresser.CanEquipItem(_item);
        ServiceLocator.Instance.CharacterDresser.OnItemEquiped += OnItemEquiped;

        _selectedImage.gameObject.SetActive(false);
        _selectedFrameImage.gameObject.SetActive(false);
        _equipedImage.gameObject.SetActive(false);

        _blockedColor.a = 0.7f;

        //_blockImage.gameObject.SetActive(!_canEquip);
        SetEquipState(_canEquip);

        ItemIcon.OnClickOnItem += ItemIcon_OnClickOnItem;
    }

    private void ItemIcon_OnClickOnItem(CharacterItemSo itemSo)
    {
        if (itemSo != _item)
        {
            _selectedImage.gameObject.SetActive(false);
            _selectedFrameImage.gameObject.SetActive(false);
        }   
    }

    private void OnItemEquiped(CharacterItemSo itemSo)
    {
        _canEquip = ServiceLocator.Instance.CharacterDresser.CanEquipItem(_item);
        //_blockImage.gameObject.SetActive(!_canEquip);
        SetEquipState(_canEquip);
        if (itemSo == _item)
        {
            _equipedImage.gameObject.SetActive(true);
            _isEquipped = true;
        }
    }

    public void OnInitializePotentialDrag(PointerEventData eventData)
    {
        eventData.useDragThreshold = true;
        eventData.tangentialPressure = _dragThreshold;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        OnIconClick();
        if (_canEquip && !_isEquipped)
        {
            OnBeginDragIcon?.Invoke();
            _greenHaloDragImage.gameObject.SetActive(true);
            _greenHaloDragImage.transform.SetParent(_canvasController.gameObject.transform);
            _greenHaloDragImage.transform.SetAsLastSibling();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_canEquip && !_isEquipped)
        {
            _dragRectTransform.position = eventData.position;
            RaycastToCanvas();
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (_canEquip && !_isEquipped)
        {
            if (_dragOnCharacter)
                OnDragItemOnCharacterIcon?.Invoke(_item);

            OnEndDragIcon?.Invoke();
            _greenHaloDragImage.gameObject.SetActive(false);
            _greenHaloDragImage.transform.SetParent(this.transform);
            _greenHaloDragImage.transform.SetAsFirstSibling();
            _dragRectTransform.localPosition = Vector3.zero;
        }
    }

    public void OnPointerClick(PointerEventData eventData) =>
        OnIconClick();

    private void OnIconClick()
    {
        _itemHolder.ShowItem(_item.ItemPrefab);
        _selectedImage.gameObject.SetActive(true);
        _selectedFrameImage.gameObject.SetActive(true);
        OnClickOnItem?.Invoke(_item);
    }

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
                    OnHoverEnterDragIcon?.Invoke();
                    Debug.Log("навели на персонажа");
                }
            }
            else
            {
                if (_dragOnCharacter)
                {
                    _dragOnCharacter = false;
                    OnHoverExitDragIcon?.Invoke();
                    Debug.Log("убрали с персонажа");
                }
            }
        }
        else
        {
            if (_dragOnCharacter)
            {
                _dragOnCharacter = false;
                OnHoverExitDragIcon?.Invoke();
                Debug.Log("убрали с персонажа");
            }
        }
    }

    private void SetEquipState(bool canEquip)
    {
        if (canEquip)
            _itemIconImage.color = _fullColor;
        else
            _itemIconImage.color = _blockedColor;
        _blockImage.gameObject.SetActive(!canEquip);
    }

    private void OnDestroy()
    {
        ServiceLocator.Instance.CharacterDresser.OnItemEquiped -= OnItemEquiped;
        ItemIcon.OnClickOnItem -= ItemIcon_OnClickOnItem;
    }
}