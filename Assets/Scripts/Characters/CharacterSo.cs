using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "ScriptableObjects/Character", order = 1)]
public class CharacterSo : ScriptableObject
{
    public string Id;

    public string CharacterName;
    public string CharacterProf;
    public string WinDescription;

    [TextArea(1, 10)] public string Task;
    [TextArea(1, 10)] public string Purpose;

    public GameObject CharacterPrefab;
    public Sprite CharacterSprite;
    public Sprite CharacterFullLength;

    public AudioClip CharacterTaskAudioClip;
    public AudioClip WinAudioClip;

    public CharacterItemSo[] Items;

    public Sprite CharacterBackground;
}