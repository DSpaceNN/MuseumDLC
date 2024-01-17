using UnityEngine;
using UnityEngine.EventSystems;

public class MainCharacterRawImageBehaviour : MonoBehaviour, IPointerMoveHandler
{
    public void OnPointerMove(PointerEventData eventData)
    {
        if (eventData.eligibleForClick && eventData.pointerEnter == this.gameObject)
        {
            ServiceLocator.Instance.InputFromImagesService.SetCharacterInput(eventData.delta);
        }
    }
}