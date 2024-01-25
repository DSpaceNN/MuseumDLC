using UnityEngine;

public class ExitPopupPanelBehaviour : PopupPanelBehaviour
{
    public override void OnApprovalButtonClick() =>
        Application.Quit();
}