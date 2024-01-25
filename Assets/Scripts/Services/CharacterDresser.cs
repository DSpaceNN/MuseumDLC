using System;
using System.Linq;
using UnityEngine;

public class CharacterDresser
{
    public CharacterDresser(CharacterOnSceneHolder characterHolder, CharacterChanger characterChanger)
    {
        _characterChanger = characterChanger;
        _characterChanger.ShowNewCharacter += OnShowNewCharacter;

        _characterOnSceneHolder = characterHolder;
        _characterOnSceneHolder.OnInstantiateCharacter += OnInstantiateCharacter;

        ItemIcon.OnDragItemOnCharacterIcon += OnDragItemOnCharacter;
    }

    public CharacterSo CurrentCharacter { get; private set; }
    public CharacterModelMb CurrentCharacterMb { get; private set; }
    public int CharacterDressCounter { get; private set; }

    public event Action<CharacterItemSo> OnItemEquiped;

    private CharacterOnSceneHolder _characterOnSceneHolder;
    private CharacterChanger _characterChanger;

    public bool CanEquipItem(CharacterItemSo itemSo) =>
        CanEquipItem(itemSo, out ItemOnCharacter item);

    public bool CanEquipItem(CharacterItemSo itemSo, out ItemOnCharacter itemOnCharacter)
    {
        itemOnCharacter = CurrentCharacterMb.ItemsOnCharacter.FirstOrDefault(x => x.Id == itemSo.Id);

        if (itemOnCharacter == null)
            Debug.LogError($"Скорее всего неправильный ID у предмета {itemSo.Id};");
        if (itemOnCharacter.ItemsBeforeEquip.Count > 0)
        {
            foreach (var item in itemOnCharacter.ItemsBeforeEquip)
                if (!item.IsEquipped)
                    return false;
        }
        return true;
    }

    private void OnShowNewCharacter(CharacterSo character)
    {
        CurrentCharacter = character;
        CharacterDressCounter = 0;
    }

    private void OnInstantiateCharacter(CharacterModelMb characterMb)
    {
        CurrentCharacterMb = characterMb;
        CurrentCharacterMb.BuildItems();
    }

    private void OnDragItemOnCharacter(CharacterItemSo itemSo)
    {
        Debug.Log("закидываем на персонажа " + itemSo.name);
        string id = itemSo.Id;

        if (CanEquipItem(itemSo, out ItemOnCharacter itemsOnCharacter))
        {
            itemsOnCharacter.IsEquipped = true;
            if (itemsOnCharacter.ItemsOnModelToHideWhenDress != null && itemsOnCharacter.ItemsOnModelToHideWhenDress.Length > 0)
                foreach(var item in itemsOnCharacter.ItemsOnModelToHideWhenDress)
                    item.SetActive(false);

            if (itemsOnCharacter.ItemsOnCharacterModel != null && itemsOnCharacter.ItemsOnCharacterModel.Length > 0)
                foreach(var item in itemsOnCharacter.ItemsOnCharacterModel)
                    item.SetActive(true);

            CharacterDressCounter++;
            OnItemEquiped?.Invoke(itemSo);
        }
        else
        {
            Debug.Log("не можем закинуть");
        }
    }
}