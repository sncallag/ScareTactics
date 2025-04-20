using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Game/Character")]
public class CharacterData : ScriptableObject
{
    public string characterId;
    public string displayName;
    public GameObject characterPrefab;
}
