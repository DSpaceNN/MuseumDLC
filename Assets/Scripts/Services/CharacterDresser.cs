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

    public event Action<CharacterItemSo> OnItemEquiped;

    private CharacterOnSceneHolder _characterOnSceneHolder;
    private CharacterChanger _characterChanger;

    public bool CanEquipItem(CharacterItemSo itemSo) =>
        CanEquipItem(itemSo, out ItemOnCharacter item);

    public bool CanEquipItem(CharacterItemSo itemSo, out ItemOnCharacter itemOnCharacter)
    {
        itemOnCharacter = CurrentCharacterMb.ItemsOnCharacter.FirstOrDefault(x => x.Id == itemSo.Id);
        if (itemOnCharacter.ItemsBeforeEquip.Count > 0)
        {
            foreach (var item in itemOnCharacter.ItemsBeforeEquip)
                if (!item.IsEquipped)
                    return false;
        }
        return true;
    }

    private void OnShowNewCharacter(CharacterSo character) =>
        CurrentCharacter = character;

    private void OnInstantiateCharacter(CharacterModelMb characterMb)
    {
        CurrentCharacterMb = characterMb;
        CurrentCharacterMb.BuildItems();
    }

    private void OnDragItemOnCharacter(CharacterItemSo itemSo)
    {
        Debug.Log("закидываем на персонажа " + itemSo.name);
        string id = itemSo.Id;

        //нужно проверить, предмет уже на персонаже или нет

        if (CanEquipItem(itemSo, out ItemOnCharacter item))
        {
            item.IsEquipped = true;
            if (item.ItemOnModelToHideWhenDress != null)
                item.ItemOnModelToHideWhenDress.SetActive(false);

            item.ItemOnCharacterModel.SetActive(true);

            OnItemEquiped?.Invoke(itemSo);
        }
        else
        {
            Debug.Log("не можем закинуть");
        }
    }
}