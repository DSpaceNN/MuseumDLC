using System;
using System.Collections.Generic;
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

        ItemIcon.OnDragItemOnCharacterIcon += OnDragItenOnCharacter;
    }

    public event Action<CharacterItemSo> OnItemEquiped;
    public CharacterSo CurrentCharacter { get; private set; }
    public CharacterModelMb CurrentCharacterMb { get; private set; }

    private CharacterOnSceneHolder _characterOnSceneHolder;
    private CharacterChanger _characterChanger;

    public bool CanEquipItem(CharacterItemSo itemSo) =>
        CanEquipItem(itemSo, out WearLevel itemWearLevel, out ItemOnWearLevel item);

    public bool CanEquipItem(CharacterItemSo itemSo, out WearLevel itemWearLevel, out ItemOnWearLevel item)
    {
        List<LinkedList<WearLevel>> abstractWearLevels = CurrentCharacterMb.AbstractWearLevelGroups;
        WearLevel[] allWearLevels = CurrentCharacterMb.WearLevelsInInspector;

        //ищем уровень одежды из общего списка
        itemWearLevel = allWearLevels.FirstOrDefault(x => x.ItemsOnLevel.Any(y => y.Id == itemSo.Id));
        item = itemWearLevel.ItemsOnLevel.FirstOrDefault(x => x.Id == itemSo.Id);
        string wearLevelName = itemWearLevel.LevelGroupId;

        //ищем группу уровней где лежит этот item
        LinkedList<WearLevel> currentListOfWearLevel = abstractWearLevels.FirstOrDefault(x => x.First().LevelGroupId == wearLevelName);
        LinkedListNode<WearLevel> currentNode = currentListOfWearLevel.Find(itemWearLevel);
        LinkedListNode<WearLevel> previousNode = currentNode.Previous;

        if (previousNode == null)
            return true;

        if (previousNode.Value.LevelIsFullyEquipped())
            return true;

        return false;
    }

    private void OnShowNewCharacter(CharacterSo character) =>
        CurrentCharacter = character;

    private void OnInstantiateCharacter(CharacterModelMb characterMb)
    {
        CurrentCharacterMb = characterMb;
        CurrentCharacterMb.BuildWearLevels();
    }


    private void OnDragItenOnCharacter(CharacterItemSo itemSo)
    {
        Debug.Log("закидываем на персонажа " + itemSo.name);
        string id = itemSo.Id;

        //нужно найти предмет на персонаже
        //нужно узнать закрыт ли предыдущий уровень
        //если уровень закрыт, то показываем предмет
        //если есть предмет, который нужно убрать взамен, то убрать его

        if (CanEquipItem(itemSo, out WearLevel itemWearLevel, out ItemOnWearLevel item))
        {
            OnItemEquiped?.Invoke(itemSo);
            item.IsEquipped = true;
            if (item.ItemOnModelToHideWhenDress != null)
                item.ItemOnModelToHideWhenDress.SetActive(false);

            item.ItemOnCharacterModel.SetActive(true);
        }
        else
        {
            Debug.Log("не можем закинуть");
        }
    }
}