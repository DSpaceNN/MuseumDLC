using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterModelMb : MonoBehaviour
{
    public List<LinkedList<WearLevel>> AbstractWearLevelGroups { get; set; } = new List<LinkedList<WearLevel>>();
    public WearLevel[] WearLevelsInInspector => _wearLevels;

    [SerializeField] private WearLevel[] _wearLevels;

    public void BuildWearLevels()
    {
        //берем количество слоёв
        List<string> levelsNames = new List<string>();

        //максимальная вложенность
        int maxLevelInsert = 0;

        foreach (var level in _wearLevels)
        {
            if (!levelsNames.Contains(level.LevelGroupId))
                levelsNames.Add(level.LevelGroupId);

            if (level.LevelInsertNumber > maxLevelInsert)
                maxLevelInsert = level.LevelInsertNumber;
        }

        //раскидываем уровни одежды по вложенности
        foreach (var levelsName in levelsNames)
        {
            string levelGroupId = levelsName;

            LinkedList<WearLevel> wearLevels = new LinkedList<WearLevel>();

            //берем по порядку из общей кучи, формируем связаный список
            for (int i = 0; i <= maxLevelInsert; i++)
            {
                foreach (var level in _wearLevels)
                {
                    if (level.LevelGroupId == levelGroupId && level.LevelInsertNumber == i)
                        wearLevels.AddLast(level);
                }
            }
            AbstractWearLevelGroups.Add(wearLevels);

            //Debug.Log("wearLevels.Count = " + wearLevels.Count);
            //var tempLevel = wearLevels.First;
            //while (tempLevel != null)
            //{
            //    Debug.Log("tempLevel.ItemsOnLevel.Length = " + tempLevel.Value.ItemsOnLevel.Length);
            //    tempLevel = tempLevel.Next;
            //}
        }
    }
}

[Serializable]
public class WearLevel
{
    public string LevelGroupId;
    public int LevelInsertNumber;
    public ItemOnWearLevel[] ItemsOnLevel;

    public bool LevelIsFullyEquipped()
    {
        foreach (var item in ItemsOnLevel)
            if (!item.IsEquipped)
                return false;
        return true;
    }

    public bool HasItemOnCurrenLevel(string id)
    {
        foreach (var item in ItemsOnLevel)
            if (item.Id == id)
                return true;
        return false;
    }
}

[Serializable]
public class ItemOnWearLevel
{
    public bool IsEquipped { get; set; }
    public string Id;
    public GameObject ItemOnCharacterModel;
    public GameObject ItemOnModelToHideWhenDress;
}