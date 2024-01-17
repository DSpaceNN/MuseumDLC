using System.Linq;
using UnityEngine;

public class CharactersStorage : MonoBehaviour
{
    public CharacterSo[] Characters;

    public CharacterSo GetCharacterById(string id)
    {
        CharacterSo result = Characters.FirstOrDefault(x => x.Id == id);
        if (result == null)
            Debug.Log("нет персонажа с таким id");
        return result;
    }
}