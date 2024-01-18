using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemsPanelBehaviour : MonoBehaviour
{
    [SerializeField] private ScrollView _itemsScroll;
    [SerializeField] private Transform _scrollContentHolder;
    [SerializeField] private ItemIcon _itemIconPrefab;

    private CharacterSo _characterSo;
    private List<ItemIcon> _itemIcons = new List<ItemIcon>();

    public void Init()
    {

    }

    public void ShowIcons(CharacterSo characterSo)
    {
        for (int i = 0; i < characterSo.Items.Length; i++)
            InstantiateIcon(characterSo.Items[i]);
    }


    private void InstantiateIcon(CharacterItemSo itemSo)
    {
        ItemIcon iconMb = Instantiate(_itemIconPrefab, _scrollContentHolder);
        iconMb.ShowItem(itemSo, this);
        _itemIcons.Add(iconMb);
    }
    private void DestroyIcons()
    {

    }
}
