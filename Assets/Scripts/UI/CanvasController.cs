using UnityEngine;

public class CanvasController : MonoBehaviour
{
    [SerializeField] private PanelsStorage _panelsStorage;
    private PanelsController _panelsController;

    public void Init()
    {
        _panelsController = new PanelsController(this, _panelsStorage);
        ShowPanelById(PanelsIdHolder.ViewCharacterPanelId);
    }

    public void ShowPanelById(string id) => 
        _panelsController.ShowPanelById(id);
}
