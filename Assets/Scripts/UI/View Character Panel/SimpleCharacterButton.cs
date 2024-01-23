using UnityEngine;

public class SimpleCharacterButton : CharacterTopButton
{
    [SerializeField] private string _characterId;

    public override void Init(CanvasController canvasController)
    {
        base.Init(canvasController);

    }

    public override void OnIconClick()
    {
        Debug.Log("открываем попап панель, кешируя Action со спауном персонажа");
    }

    public override void OnCharacterChange(CharacterSo characterSo)
    {

    }
}