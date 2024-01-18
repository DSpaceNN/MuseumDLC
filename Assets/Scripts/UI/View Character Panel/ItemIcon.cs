using UnityEngine;
using UnityEngine.UI;

public class ItemIcon : MonoBehaviour
{
    [SerializeField] private Image _itemImage; 
    private CharacterItemSo _item;
    private ItemsPanelBehaviour _parentPanel;

    public void ShowItem(CharacterItemSo item, ItemsPanelBehaviour parentPanel)
    {
        _item = item;
        _parentPanel = parentPanel;
        _itemImage.sprite = _item.ItemSprite;
    }
}