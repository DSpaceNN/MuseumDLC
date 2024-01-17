using UnityEngine;

public class InputFromImagesService
{
    public Vector2 CharacterDeltaInput { get; private set; }
    public Vector2 ItemDeltaInput { get; private set; }

    private bool _inputCharacterFlag;
    private bool _inputItemFlag;

    public void SetCharacterInput(Vector2 delta)
    {
        _inputCharacterFlag = true;
        CharacterDeltaInput = delta;
    }

    public void SetItemInput(Vector2 delta)
    {
        _inputItemFlag = true;
        ItemDeltaInput = delta;
    }

    public void Update()
    {
        if (_inputCharacterFlag)
            _inputCharacterFlag = false;
        else
            CharacterDeltaInput = Vector2.zero;

        if (_inputItemFlag)
            _inputItemFlag = false;
        else
            ItemDeltaInput = Vector2.zero;
    }
}
