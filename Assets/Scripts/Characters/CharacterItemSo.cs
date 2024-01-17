using UnityEngine;

[CreateAssetMenu(fileName = "CharacterItem", menuName = "ScriptableObjects/CharacterItem", order = 1)]
public class CharacterItemSo : ScriptableObject
{
    public string Id;
    public string ItemName;
    public string ItemDescription;
    public GameObject ItemPrefab;
    public Sprite ItemSprite;
}
