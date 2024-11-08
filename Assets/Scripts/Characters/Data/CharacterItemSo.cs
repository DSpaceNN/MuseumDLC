using UnityEngine;

[CreateAssetMenu(fileName = "CharacterItem", menuName = "ScriptableObjects/CharacterItem", order = 1)]
public class CharacterItemSo : ScriptableObject
{
    public string Id;
    [TextArea(1, 10)] public string ItemName;
    [TextArea(3, 20)] public string ItemDescription;
    public GameObject ItemPrefab;
    public Sprite ItemSprite;
    public AudioClip ItemNameAudioClip;
    public AudioClip ItemAudioClip;         //TODO как начнётся заполнение - переименовать
}