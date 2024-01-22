using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterModelMb : MonoBehaviour
{
    public ItemOnCharacter[] ItemsOnCharacter => _items;

    [SerializeField] private ItemOnCharacter[] _items;

    public void BuildItems()
    {
        foreach (var item in _items)
        {
            item.ItemsBeforeEquip = new List<ItemOnCharacter>();
            if (item.ItemsBeforeEquipIds.Length > 0)
            {
                foreach (string id in item.ItemsBeforeEquipIds)
                {
                    ItemOnCharacter beforeItem = _items.FirstOrDefault(x => x.Id == id);
                    if (beforeItem == null) 
                    {
                        Debug.Log("Не нашёлся в инвентаре предмет с ID = " + id);
                        continue;
                    }
                    item.ItemsBeforeEquip.Add(beforeItem);
                }
            }
        }
    }
}