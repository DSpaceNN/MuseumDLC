using UnityEngine;

public abstract class PanelBase : MonoBehaviour
{
    protected CanvasController CanvasController { get; private set; }
    protected PanelsController PanelsController { get; private set; }

    public virtual void Initialize(CanvasController canvasController, PanelsController panelsController)
    {
        CanvasController = canvasController;
        PanelsController = panelsController;
    }

    public virtual void CleanUpPanel() { }
    public virtual void DestroyPanel() =>
        Destroy(this.gameObject);
}
