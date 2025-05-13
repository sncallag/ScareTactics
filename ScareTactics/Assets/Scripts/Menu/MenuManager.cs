using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject settingsPanel;
    private PlayerStats stats;

   
    public void NewGame()
    {
        if (System.IO.File.Exists(GameSaveManager.SavePath))
        {
            System.IO.File.Delete(GameSaveManager.SavePath);
        }
        SceneManager.LoadScene(1);
    }
    public void ContinueGame()
    {   
        PlayerSaveData savedData = GameSaveManager.LoadGame();
        if (savedData != null)
        {
          
     
            SceneManager.LoadScene(2); 
        }
        else
        {
            Debug.Log("No save file found!");
            
        }
    }

    public void Settings()
    {
        settingsPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None; 
        Cursor.visible = true;
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);

      
    }
    public void CloseSettingsGame()
    {
        settingsPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void SaveMenu()
    {
        stats = FindAnyObjectByType<PlayerStats>();
        stats.SavePlayer();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
