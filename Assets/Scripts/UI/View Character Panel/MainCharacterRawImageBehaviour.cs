using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainCharacterRawImageBehaviour : MonoBehaviour, IPointerMoveHandler, IDropHandler
{
    [SerializeField] private Image _greenCircleImage;

    private void Start()
    {
        ItemIcon.OnHoverEnterDragIcon += ItemIcon_OnHoverEnterDragIcon;
        ItemIcon.OnHoverExitDragIcon += ItemIcon_OnHoverExitDragIcon;
        _greenCircleImage.gameObject.SetActive(false);
    }

    private void ItemIcon_OnHoverExitDragIcon() =>
        _greenCircleImage.gameObject.SetActive(false);
    
    private void ItemIcon_OnHoverEnterDragIcon() =>
        _greenCircleImage.gameObject.SetActive(true);

    public void OnDrop(PointerEventData eventData) =>
        _greenCircleImage.gameObject.SetActive(false);

    public void OnPointerMove(PointerEventData eventData)
    {
        if (eventData.eligibleForClick && eventData.pointerEnter == this.gameObject)
            ServiceLocator.Instance.InputFromImagesService.SetCharacterInput(eventData.delta);
    }
}