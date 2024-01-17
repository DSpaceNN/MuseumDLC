using UnityEngine;
using UnityEngine.UI;

public class ViewCharacterPanelBehaviour : PanelBase, IMainPanel
{
    [SerializeField] private RawImage _mainCharacterImage;
    [SerializeField] private MainCharacterRawImageBehaviour _mainCharacterIcon;

    public override void Initialize(CanvasController canvasController, PanelsController panelsController)
    {
        base.Initialize(canvasController, panelsController);
    }
}