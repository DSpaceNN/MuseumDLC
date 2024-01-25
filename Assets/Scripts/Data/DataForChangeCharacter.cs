using System;

public class DataForChangeCharacter : IPanelBaseData
{
    public DataForChangeCharacter(Action changeCharecterAction)
    {
        ChangeCharacterAction = changeCharecterAction;
    }

    public Action ChangeCharacterAction;
}