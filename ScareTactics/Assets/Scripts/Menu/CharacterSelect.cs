using UnityEngine;

public class CharacterSelectManager : MonoBehaviour
{
    [Header("Character Setup")]
    public GameObject[] characterModels;          
    public CharacterData[] characterDataList;    

    [Header("Selection UI")]
    public CharacterSelectButton selectButton;     

    private int currentIndex = 0;

    void Start()
    {
        UpdateCharacterDisplay();
    }

    public void NextCharacter()
    {
        characterModels[currentIndex].SetActive(false);
        currentIndex = (currentIndex + 1) % characterModels.Length;
        UpdateCharacterDisplay();
    }

    public void PreviousCharacter()
    {
        characterModels[currentIndex].SetActive(false);
        currentIndex--;
        if (currentIndex < 0)
            currentIndex = characterModels.Length - 1;

        UpdateCharacterDisplay();
    }

    private void UpdateCharacterDisplay()
    {
        // Turn on only the current model
        for (int i = 0; i < characterModels.Length; i++)
        {
            characterModels[i].SetActive(i == currentIndex);
        }

        // Update select button with the correct CharacterData
        selectButton.characterToSelect = characterDataList[currentIndex];
    }
}
