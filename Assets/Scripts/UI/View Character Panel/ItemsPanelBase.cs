using System;
using UnityEngine;

public class ItemsPanelBase : MonoBehaviour
{
    protected CanvasController _canvasController;

    public virtual void Init(CanvasController canvasController)
    {
        _canvasController = canvasController;
    }

    public virtual void ShowIcons(CharacterSo characterSo)
    {

    }

    protected virtual void OnQuestionButton()
    {
        OnQuestionButtonClick?.Invoke();
    }

    protected virtual void OnInfoButton()
    {
        OnInfoButtonClick?.Invoke();
    }

    public static event Action OnQuestionButtonClick;
    public static event Action OnInfoButtonClick;
}