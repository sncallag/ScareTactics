using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelectButton : MonoBehaviour
{
    public CharacterData characterToSelect;
    public GameObject startButton;

    private void Start()
    {
        startButton.SetActive(false);
    }
    public void OnSelectButtonClicked()
    {
        PlayerSelectionManager.Instance.selectedCharacter = characterToSelect;
        PlayerPrefs.SetString("SelectedCharacterID", characterToSelect.characterId);
        PlayerPrefs.Save();
        startButton.SetActive(true);
    }

    public void NextScene()
    {
        SceneManager.LoadScene(2);
    }
}
