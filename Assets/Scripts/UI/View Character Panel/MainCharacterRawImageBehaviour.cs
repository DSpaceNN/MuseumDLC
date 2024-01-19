using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainCharacterRawImageBehaviour : MonoBehaviour, IPointerMoveHandler, IDropHandler
{
    public void OnPointerMove(PointerEventData eventData)
    {
        if (eventData.eligibleForClick && eventData.pointerEnter == this.gameObject)
        {
            ServiceLocator.Instance.InputFromImagesService.SetCharacterInput(eventData.delta);
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("MainCharacterRawImageBehaviour IDropHandler");
    }
}