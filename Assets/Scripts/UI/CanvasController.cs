using UnityEngine;

public class CanvasController : MonoBehaviour
{
    [SerializeField] private PanelsStorage _panelsStorage;

    private PanelsController _panelsController;
    private ServiceLocator _serviceLocator;

    public void Init()
    {
        _panelsController = new PanelsController(this, _panelsStorage);
        _serviceLocator  = ServiceLocator.Instance;
    }
        
    public void ShowStartPanel()
    {
        switch (_serviceLocator.InterfaceType)
        {
            case Enums.InterfaceType.DarkTheme:
                ShowPanelById(PanelsIdHolder.StartMenuPanelPanelId);
                break;

            case Enums.InterfaceType.WhiteTheme:
                ShowPanelById(PanelsIdHolder.StartWhiteScreenId);
                break;
        }
    }
    
    public void ShowPanelById(string id, IPanelBaseData dataForOpen = null) => 
        _panelsController.ShowPanelById(id, dataForOpen);
}