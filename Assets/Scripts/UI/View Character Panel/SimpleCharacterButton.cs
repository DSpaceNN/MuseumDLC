using System;
using UnityEngine;

public class SimpleCharacterButton : CharacterTopButton
{
    [SerializeField] private string _characterId;

    public override void Init(CanvasController canvasController)
    {
        base.Init(canvasController);
        CharacterId = _characterId;
        OnCharacterChange(ServiceLocator.Instance.CharacterChanger.CurrentCharacter);
    }

    public override void OnIconClick()
    {
        if (ServiceLocator.Instance.CharacterDresser.CharacterDressCounter == 0)
        {
            ServiceLocator.Instance.CharacterChanger.ShowCharacterById(_characterId);
        }
        else
        {
            Action action = () => ServiceLocator.Instance.CharacterChanger.ShowCharacterById(_characterId);
            DataForChangeCharacter data = new DataForChangeCharacter(action);
            CanvasController.ShowPanelById(PanelsIdHolder.ChooseAnotherCharacterPopupPanelId, data);
        }
    }
}