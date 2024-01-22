using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemOnCharacter
{
    public List<ItemOnCharacter> ItemsBeforeEquip { get; set; }
    public bool IsEquipped { get; set; }

    public string Id;
    public string[] ItemsBeforeEquipIds;
    public GameObject ItemOnCharacterModel;
    public GameObject ItemOnModelToHideWhenDress;
}