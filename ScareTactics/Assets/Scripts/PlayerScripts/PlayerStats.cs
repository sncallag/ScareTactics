using UnityEngine;


public class PlayerStats : MonoBehaviour
{
    public string characterId;
    public float health = 100f;
    public int level = 1;
    public int experience = 0;
    public int xpToLevel = 30;
    public static PlayerStats Instance;
    private EndGameEvent endGameEvent;

    private void Start()
    {
        Instance = this;
        endGameEvent = FindObjectOfType<EndGameEvent>();
       

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SavePlayer();
        }
    }
    public void SavePlayer()
    {
        PlayerSaveData data = new PlayerSaveData(characterId, health, level, experience, transform.position);
        GameSaveManager.SaveGame(data);
    }

    public void LoadPlayer()
    {
        PlayerSaveData data = GameSaveManager.LoadGame();
        if (data != null)
        {
            characterId = data.characterId;
            health = data.health;
            level = data.level;
            experience = data.experience;
            transform.position = data.savedPosition;
            Debug.Log("Player loaded successfully");
        }
        else
        {
            Debug.LogWarning("No save data found.");
        }
    }


    public void GainXP(int amount)
    {
        experience += amount;
        Debug.Log($"{gameObject.name} gained {amount} XP! Total XP: {experience}");
        if (experience >= 10)
        {
            DialogueManager.Instance.SetCondition("LevelUp", true);
        }

        if (experience >= xpToLevel)
        {
            LevelUp();
           
        }
    }

    private void LevelUp()
    {
        level++;
        if (level == 2)
        {
            endGameEvent.Level2();
        }
        if (level == 3)
        {
            endGameEvent.Level3();
        }
        if (level >= 3)
        {
            DialogueManager.Instance.SetCondition("Level3", true);
        }

        experience -= xpToLevel; 
        Debug.Log($"{gameObject.name} leveled up! New level: {level}");

        health += 10f;
        xpToLevel += 10;

       

        SavePlayer(); 
    }


}
