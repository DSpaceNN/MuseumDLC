using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "ScriptableObjects/Character", order = 1)]
public class CharacterSo : ScriptableObject
{
    public string Id;
    public string CharacterName;
    public string CharacterDescription;

    public string Task;
    public string Purpose;

    public GameObject CharacterPrefab;
    public Sprite CharacterSprite;
    public AudioClip CharacterTaskAudioClip;
    public CharacterItemSo[] Items;
}