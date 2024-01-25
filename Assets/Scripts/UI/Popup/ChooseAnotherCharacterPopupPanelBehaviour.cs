public class ChooseAnotherCharacterPopupPanelBehaviour : PopupPanelBehaviour
{
    public override void OnApprovalButtonClick()
    {
        if (DataForOpenPanel != null)
        {
            DataForChangeCharacter data = DataForOpenPanel as DataForChangeCharacter;
            data.ChangeCharacterAction?.Invoke();
        }
    }
}