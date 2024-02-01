using System;

public class CharacterChanger
{
    public CharacterSo CurrentCharacter { get; private set; }

    public event Action<CharacterSo> ShowNewCharacter;

    public void ResetCharacter() =>
        ShowCharacterById(CurrentCharacter.Id);

    public void ShowCharacterById(string id)
    {
        CharacterSo characterSo = ServiceLocator.Instance.CharactersStorage.GetCharacterById(id);
        if (characterSo == null)
            return;

        CurrentCharacter = characterSo;
        ShowNewCharacter?.Invoke(characterSo);
    }
}