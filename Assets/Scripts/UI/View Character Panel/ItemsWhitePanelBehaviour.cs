using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemsWhitePanelBehaviour : ItemsPanelBase
{
    [SerializeField] private ScrollView _itemsScroll;
    [SerializeField] private Transform _scrollContentHolder;
    [SerializeField] private ItemIcon _itemIconPrefab;

    private List<ItemIcon> _itemIcons = new List<ItemIcon>();

    public override void Init(CanvasController canvasController)
    {
        base.Init(canvasController);
    }

    public override void ShowIcons(CharacterSo characterSo)
    {
        Tools.DestroyAllChilds(_scrollContentHolder);
        _itemIcons.Clear();

        for (int i = 0; i < characterSo.Items.Length; i++)
            InstantiateIcon(characterSo.Items[i]);
    }

    public void InstantiateIcon(CharacterItemSo itemSo)
    {
        ItemIcon iconMb = Instantiate(_itemIconPrefab, _scrollContentHolder);
        iconMb.ShowItem(itemSo, _canvasController);
        _itemIcons.Add(iconMb);
    }
}