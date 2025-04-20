using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public CharacterData[] allCharacters; // Drag all CharacterData assets into this array via Inspector

    private void Start()
    {
        PlayerSaveData savedData = GameSaveManager.LoadGame();

        if (savedData != null)
        {
            CharacterData savedCharacter = GetCharacterById(savedData.characterId);

            if (savedCharacter != null)
            {
                GameObject player = Instantiate(savedCharacter.characterPrefab, savedData.savedPosition, Quaternion.identity);
                Debug.Log("Spawned saved character: " + savedCharacter.characterId);

                PlayerStats playerStats = player.GetComponent<PlayerStats>();
                if (playerStats != null)
                {
                    ApplySavedDataToPlayer(playerStats, savedData);
                    Debug.Log("Applied saved data to player.");
                }
                else
                {
                    Debug.LogWarning("PlayerStats component not found on spawned character.");
                }
            }
            else
            {
                Debug.LogWarning("Saved character ID not found in available CharacterData list.");
            }
        }
        else
        {
            // Fallback to selected character from selection screen
            CharacterData selected = PlayerSelectionManager.Instance?.selectedCharacter;
            if (selected != null)
            {
                GameObject player = Instantiate(selected.characterPrefab, transform.position, Quaternion.identity);
                Debug.Log("Spawned selected character: " + selected.characterId);

                PlayerStats playerStats = player.GetComponent<PlayerStats>();
                if (playerStats != null)
                {
                    playerStats.characterId = selected.characterId;
                    Debug.Log("Assigned characterId to newly selected player.");
                }
            }
            else
            {
                Debug.LogWarning("No character selected or saved.");
            }
        }
    }

    private void ApplySavedDataToPlayer(PlayerStats playerStats, PlayerSaveData savedData)
    {
        playerStats.characterId = savedData.characterId;
        playerStats.xpToLevel = savedData.experience;
        playerStats.level = savedData.level;
        playerStats.health = savedData.health;
        playerStats.transform.position = savedData.savedPosition;

    }

    private CharacterData GetCharacterById(string id)
    {
        foreach (var character in allCharacters)
        {
            if (character.characterId == id)
                return character;
        }
        return null;
    }
}
