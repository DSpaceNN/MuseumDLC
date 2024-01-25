public class ResetGamePopupPanelBehaviour : PopupPanelBehaviour
{
    public override void OnApprovalButtonClick() =>
        ServiceLocator.Instance.CharacterChanger.ResetCharacter();
}