using System.Collections.Generic;
using UnityEngine;

public class ItemOnCharacterMb : MonoBehaviour
{
    public List<ItemOnCharacterMb> ItemsBeforeEquip { get; set; }
    public bool IsEquipped { get; set; }

    public string Id;
    public string[] ItemsBeforeEquipIds;
    public GameObject[] ItemsOnCharacterModel;
    public GameObject[] ItemsOnModelToHideWhenDress;

    public virtual void OnEquip() { }
}