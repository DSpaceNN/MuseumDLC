using UnityEngine;

public class TopButtonsPanel : MonoBehaviour
{
    [SerializeField] private CharacterTopButton[] _characterButtons;
    private CanvasController _canvasController;

    public void Init(CanvasController canvasController)
    {
        _canvasController = canvasController;

        foreach (var button in _characterButtons)
            button.Init(_canvasController);
    }
}