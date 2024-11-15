using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemsWhitePanelBehaviour : MonoBehaviour
{
    [SerializeField] private ScrollView _itemsScroll;
    [SerializeField] private Transform _scrollContentHolder;
    [SerializeField] private ItemIcon _itemIconPrefab;
    private CanvasController _canvasController;

    private List<ItemIcon> _itemIcons = new List<ItemIcon>();

    public void Init(CanvasController canvasController) =>
        _canvasController = canvasController;

    public void ShowIcons(CharacterSo characterSo)
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