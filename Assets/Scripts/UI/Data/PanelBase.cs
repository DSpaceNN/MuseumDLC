using UnityEngine;

public abstract class PanelBase : MonoBehaviour
{
    protected CanvasController CanvasController { get; private set; }
    protected PanelsController PanelsController { get; private set; }
    protected IPanelBaseData DataForOpenPanel { get; private set; }

    public virtual void Initialize(CanvasController canvasController, PanelsController panelsController, IPanelBaseData dataForOpen = null)
    {
        CanvasController = canvasController;
        PanelsController = panelsController;
        DataForOpenPanel = dataForOpen;
    }

    public virtual void CleanUpPanel() { }
    public virtual void DestroyPanel() =>
        Destroy(this.gameObject);
}
