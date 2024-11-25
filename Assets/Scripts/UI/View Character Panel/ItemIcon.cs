using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemIcon : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    public static event Action OnBeginDragIcon;
    public static event Action OnEndDragIcon;

    public static event Action OnHoverEnterDragIcon;
    public static event Action OnHoverExitDragIcon;

    public static event Action<CharacterItemSo> OnDragItemOnCharacterIcon;
    public static event Action<CharacterItemSo> OnChooseItem;

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
    private ItemOnSceneHolder _itemHolder;
    private CanvasController _canvasController;
    private InputService _inputService;
    private EventSystem _eventSystem;

    private bool _canEquip;
    private bool _isEquipped;
    private bool _dragOnCharacter;
    private bool _isCheckingForChoose;
    private bool _isDragging;

    private const float _durationForChoose = 0.2f;
    private float _timerForChoose = 0;
    private float _deltaInputTheshold = 5f;

    public void ShowItem(CharacterItemSo item, CanvasController canvasController)
    {
        _eventSystem = EventSystem.current;
        _inputService = ServiceLocator.Instance.InputService;

        _item = item;
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

        ItemIcon.OnChooseItem += ItemIcon_OnChooseItem;
        _inputService.OnPointerUp += OnEndDragFromInputSystem;
    }

    private void ItemIcon_OnChooseItem(CharacterItemSo itemSo)
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

    public void OnPointerClick(PointerEventData eventData) =>
        OnIconClick();

    private void OnIconClick()
    {
        ChooseItem();
    }

    private void ChooseItem()
    {
        _itemHolder.ShowItem(_item.ItemPrefab);
        _selectedImage.gameObject.SetActive(true);
        _selectedFrameImage.gameObject.SetActive(true);
        OnChooseItem?.Invoke(_item);
    }

    private void RaycastToCanvas()
    {
        List<RaycastResult> results = new List<RaycastResult>();
        var pointerEventData = new PointerEventData(_eventSystem);
        pointerEventData.position = _inputService.InputPosition;

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
        ItemIcon.OnChooseItem -= ItemIcon_OnChooseItem;
        _inputService.OnPointerUp -= OnEndDragFromInputSystem;
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        _isCheckingForChoose = true;
    }

    private void Update()
    {
        HandleTimerForChoose();
        HandleDrag();
    }

    private void HandleTimerForChoose()
    {
        if (_isCheckingForChoose)
        {
            _timerForChoose += Time.deltaTime;
            Debug.Log("_inputService.InputDeltaResult.magnitude = " + _inputService.InputDeltaResult.magnitude);
            if (_inputService.InputDeltaResult.magnitude > _deltaInputTheshold)
                FailToChoose();

            if (_timerForChoose > _durationForChoose)
                ChooseIcon();
        }
    }

    private void FailToChoose()
    {
        Debug.Log("FailToChoose");
        _isCheckingForChoose = false;
        ResetChoose();
    }

    private void ChooseIcon()
    {
        Debug.Log("Choose Icon");
        ResetChoose();
        _isDragging = true;

        _dragRectTransform.position = _inputService.InputPosition;

        OnIconClick();
        if (_canEquip && !_isEquipped)
        {
            OnBeginDragIcon?.Invoke();
            _greenHaloDragImage.gameObject.SetActive(true);
            _greenHaloDragImage.transform.SetParent(_canvasController.gameObject.transform);
            _greenHaloDragImage.transform.SetAsLastSibling();
        }
    }

    private void HandleDrag()
    {
        if (_isDragging)
        {
            if (_canEquip && !_isEquipped)
            {
                _dragRectTransform.position = _inputService.InputPosition;
                RaycastToCanvas();
            }
        }
    }

    private void ResetChoose()
    {
        _isCheckingForChoose = false;
        _timerForChoose = 0;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (_isCheckingForChoose)
            ResetChoose();
    }

    private void OnEndDragFromInputSystem(Vector2 position)
    {
        if (_isDragging && _canEquip && !_isEquipped)
        {
            if (_dragOnCharacter)
                OnDragItemOnCharacterIcon?.Invoke(_item);
            //TODO MissingReferenceException: The object of type 'Text' has been destroyed but you are still trying to access it.
            //Your script should either check if it is null or you should not destroy the object.

            OnEndDragIcon?.Invoke();
            _greenHaloDragImage.gameObject.SetActive(false);
            _greenHaloDragImage.transform.SetParent(this.transform);
            _greenHaloDragImage.transform.SetAsFirstSibling();
            _dragRectTransform.localPosition = Vector3.zero;
        }
    }
}