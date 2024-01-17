using UnityEngine;
using UnityEngine.EventSystems;

public class ItemRawImageBehaviour : MonoBehaviour, IPointerMoveHandler
{
    public void OnPointerMove(PointerEventData eventData)
    {
        if (eventData.eligibleForClick && eventData.pointerEnter == this.gameObject)
        {
            ServiceLocator.Instance.InputFromImagesService.SetItemInput(eventData.delta);
        }
    }
}