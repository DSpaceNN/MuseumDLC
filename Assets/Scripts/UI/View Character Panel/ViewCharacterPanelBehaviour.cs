using UnityEngine;
using UnityEngine.UI;

public class ViewCharacterPanelBehaviour : PanelBase, IMainPanel
{
    [SerializeField] private RawImage _mainCharacterImage;
    [SerializeField] private MainCharacterRawImageBehaviour _mainCharacterIcon;
    [SerializeField] private ItemsPanelBehaviour _itemsPanel;

    private CharacterChanger _characterChanger;

    public override void Initialize(CanvasController canvasController, PanelsController panelsController, IPanelBaseData dataForOpen = null)
    {
        base.Initialize(canvasController, panelsController, dataForOpen);

        _itemsPanel.Init(canvasController);
        _characterChanger = ServiceLocator.Instance.CharacterChanger;
        _characterChanger.ShowNewCharacter += OnChangeCharacter;
    }

    private void OnChangeCharacter(CharacterSo characterSo) =>
        _itemsPanel.ShowIcons(characterSo);

    private void OnDestroy() =>
        _characterChanger.ShowNewCharacter -= OnChangeCharacter;
}