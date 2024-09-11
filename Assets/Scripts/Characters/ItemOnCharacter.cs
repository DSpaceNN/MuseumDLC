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
    public GameObject[] ItemsOnCharacterModel;
    public GameObject[] ItemsOnModelToHideWhenDress;

    //TODO не прокатит, надо думать, переписывать
    //может быть всё-таки на монобехи в айтеме, всё равно все модельки переделывать
    public virtual void OnEquip() { }
}