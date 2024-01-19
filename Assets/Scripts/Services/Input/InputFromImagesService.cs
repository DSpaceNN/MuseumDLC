using UnityEngine;

public class InputFromImagesService
{
    public Vector2 CharacterDeltaInput { get; private set; }
    public Vector2 ItemDeltaInput { get; private set; }

    private bool _disableInput;
    private bool _inputCharacterFlag;
    private bool _inputItemFlag;

    public void Init()
    {
        ItemIcon.OnBeginDragIcon += OnBeginDragItemIcon;
        ItemIcon.OnEndDragIcon += OnEndDragItemIcon;
    }

    private void OnBeginDragItemIcon() =>
        _disableInput = true;

    private void OnEndDragItemIcon() =>
        _disableInput = false;

    public void SetCharacterInput(Vector2 delta)
    {
        _inputCharacterFlag = true;

        if (_disableInput)
            CharacterDeltaInput = Vector2.zero;
        else
            CharacterDeltaInput = delta;
    }

    public void SetItemInput(Vector2 delta)
    {
        _inputItemFlag = true;

        if (_disableInput)
            ItemDeltaInput = Vector3.zero;
        else
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