using UnityEngine;
using UnityEngine.UI;

public abstract class PopupPanelBehaviour : PanelBase, IPopupPanel
{
    [SerializeField] private Button _approvalButton;
    [SerializeField] private Button _cancelButton;

    public override void Initialize(CanvasController canvasController, PanelsController panelsController, IPanelBaseData dataForOpen = null)
    {
        base.Initialize(canvasController, panelsController, dataForOpen);

        _approvalButton.onClick.AddListener(() => { OnApprovalButtonClick(); DestroyPanelFromController(); });
        _cancelButton.onClick.AddListener(() => OnCancelButtonClick());
    }

    public virtual void OnApprovalButtonClick() { }

    public virtual void OnCancelButtonClick() =>
         DestroyPanelFromController();

    private void DestroyPanelFromController() =>
        PanelsController.DeletePanelOnScene(this.GetType());
}