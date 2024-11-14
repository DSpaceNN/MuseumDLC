using System;
using UnityEngine;

public class CharacterIconBase : MonoBehaviour
{
    public static Action<string> OnStartCharacterIconClick;
    public string CharacterId { get; private set; }

    public virtual void Init(string characterId)
    {
        CharacterId = characterId;
        CharacterIconBase.OnStartCharacterIconClick += OnCharacterIconClick;
    }

    public virtual void SetChoosenState() =>
        OnStartCharacterIconClick?.Invoke(CharacterId);

    protected virtual void OnCharacterIconClick(string characterId)
    {
        if (characterId == CharacterId)
            ShowChoosenState();
        else
            ShowUnchoosenState();
    }

    protected virtual void ShowChoosenState() { }
    protected virtual void ShowUnchoosenState() { }

    protected virtual void OnDestroy()
    {
        CharacterIconBase.OnStartCharacterIconClick -= OnCharacterIconClick;
    }
}