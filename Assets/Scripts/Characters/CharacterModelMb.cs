using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterModelMb : MonoBehaviour
{
    public ItemOnCharacterMb[] ItemsOnCharacter => _itemsMb;

    [SerializeField] private ItemOnCharacterMb[] _itemsMb;

    public void BuildItems()
    {
        foreach (var item in _itemsMb)
        {
            item.ItemsBeforeEquip = new List<ItemOnCharacterMb>();
            if (item.ItemsBeforeEquipIds.Length > 0)
            {
                foreach (string id in item.ItemsBeforeEquipIds)
                {
                    ItemOnCharacterMb beforeItem = _itemsMb.FirstOrDefault(x => x.Id == id);
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