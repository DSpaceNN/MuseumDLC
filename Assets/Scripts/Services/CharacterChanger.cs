using System;
using UnityEngine;

public class CharacterChanger
{
    public CharacterSo CurrentCharacter { get; private set; }

    public event Action<CharacterSo> ShowNewCharacter;

    public void ShowStartCharacter() =>
        ShowCharacterById("TestMan");

    public void ShowCharacterById(string id)
    {
        CharacterSo characterSo = ServiceLocator.Instance.CharactersStorage.GetCharacterById(id);
        if (characterSo == null)
            return;

        if (CurrentCharacter != null)
            CurrentCharacter = characterSo;

        ShowNewCharacter?.Invoke(characterSo);
    }
}