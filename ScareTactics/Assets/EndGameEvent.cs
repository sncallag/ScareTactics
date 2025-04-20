using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;
public class EndGameEvent : MonoBehaviour
{
    public GameObject levelUpPanel;
    public TMP_Text levelUpMessage;
    public float displayTime = 3f;

    private void Start()
    {
        levelUpPanel.SetActive(false);
    }

    public void Level2()
    {
        ShowMessage("You reached Level 2! You can now throw larger items such as trash cans");
    }

    public void Level3()
    {
        ShowMessage("Level 3 unlocked! I should probably go check back in.");
    }

    private void ShowMessage(string message)
    {
        StartCoroutine(ShowLevelUpMessage(message));
    }

    private IEnumerator ShowLevelUpMessage(string message)
    {
        levelUpMessage.text = message;
        levelUpPanel.SetActive(true);
        yield return new WaitForSeconds(displayTime);
        levelUpPanel.SetActive(false);
    }


    public void ExitGame()
    {
        Application.Quit();
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
