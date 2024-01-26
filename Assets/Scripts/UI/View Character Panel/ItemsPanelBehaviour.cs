using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class ItemsPanelBehaviour : MonoBehaviour
{
    [SerializeField] private ScrollView _itemsScroll;
    [SerializeField] private Transform _scrollContentHolder;
    [SerializeField] private ItemIcon _itemIconPrefab;
    [SerializeField] private Button _questionButton;

    private CanvasController _canvasController;
    private ViewCharacterPanelBehaviour _characterPanelMb;
    private CharacterSo _characterSo;
    private List<ItemIcon> _itemIcons = new List<ItemIcon>();

    public void Init(CanvasController canvasController, ViewCharacterPanelBehaviour characterPanelMb)
    {
        _canvasController = canvasController;
        _characterPanelMb = characterPanelMb;
        _questionButton.onClick.AddListener(() => characterPanelMb.ShowCharacterInfoPanel());
    }

    public void ShowIcons(CharacterSo characterSo)
    {
        Tools.DestroyAllChilds(_scrollContentHolder);
        _itemIcons.Clear();

        for (int i = 0; i < characterSo.Items.Length; i++)
            InstantiateIcon(characterSo.Items[i]);
    }

    private void InstantiateIcon(CharacterItemSo itemSo)
    {
        ItemIcon iconMb = Instantiate(_itemIconPrefab, _scrollContentHolder);
        iconMb.ShowItem(itemSo, this, _canvasController);
        _itemIcons.Add(iconMb);
    }
}