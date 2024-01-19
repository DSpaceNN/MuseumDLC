using System;
using UnityEngine;

public class CharacterModelMb : MonoBehaviour
{
    public WearLevel[] _wearLevels;
}

[Serializable]
public class WearLevel
{
    public string LevelName;
    public string LowerLevel;
    public ItemOnWearLevel[] ItemsOnLevel;
}

[Serializable]
public class ItemOnWearLevel
{
    public string Id;
    public GameObject ItemOnCharacterModel;
    public GameObject ItemOnModelToHideWhenDress;
}