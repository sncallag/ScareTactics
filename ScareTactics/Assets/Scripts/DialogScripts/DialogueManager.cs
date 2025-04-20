using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;  
    [SerializeField] private TMP_Text dialogText;
    [SerializeField] private GameObject dialogPanel;
    [SerializeField] private TMP_Text responseText;

    private DialogueData currentDialogue;
    private int currentLineIndex = 0;
    private string currentNPCID;
    public Dictionary<string, bool> dialogConditions = new Dictionary<string, bool>();
    private bool waitingForReaction = false;
    private bool waitingForPlayerChoices = false;
    private bool dialogueEnded = false;
    public GameObject endGamePanel;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
            
        else
        {
            Destroy(gameObject);
        }
            
        dialogPanel.SetActive(false);
        endGamePanel.SetActive(false);
    }

    private void Update()
    {
        if (currentDialogue == null) return;

        if (waitingForReaction && Input.GetKeyDown(KeyCode.Space))
        {
            waitingForReaction = false;
            NextLine();
            return;
        }

        if (waitingForPlayerChoices && Input.GetKeyDown(KeyCode.Space))
        {
            waitingForPlayerChoices = false; // Now show responses
            ShowPlayerResponses();
            return;
        }

        if (!waitingForReaction && !waitingForPlayerChoices)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1)) HandleResponse(0);
            if (Input.GetKeyDown(KeyCode.Alpha2)) HandleResponse(1);
            if (Input.GetKeyDown(KeyCode.Alpha3)) HandleResponse(2);
            if (Input.GetKeyDown(KeyCode.Space)) NextLine();
        }
    }
    public void StartDialogue(DialogueData dialogue, string npcID)
    {
        currentDialogue = dialogue;
        currentNPCID = npcID;
        currentLineIndex = 0;
        dialogPanel.SetActive(true);

        // Look for a valid conditional line first
        for (int i = 0; i < currentDialogue.dialoglines.Count; i++)
        {
            var line = currentDialogue.dialoglines[i];

            if (line.isConditional && dialogConditions.ContainsKey(line.conditionKey) && dialogConditions[line.conditionKey])
            {
                currentLineIndex = i; // Jump to this line
                ShowDialog();
                return; // Stop searching
            }
        }

        // If no valid conditional lines were found, start normally
        ShowDialog();
    }


    private void ShowDialog()
    {
        if (currentDialogue == null || currentLineIndex >= currentDialogue.dialoglines.Count) return;

        var line = currentDialogue.dialoglines[currentLineIndex];

        if (line.isConditional && !dialogConditions.ContainsKey(line.conditionKey))
        {
            currentLineIndex++;
            ShowDialog();
            return;
        }

        // Show NPC dialogue first
        dialogText.text = line.lineText;
        responseText.text = ""; // Hide responses initially

        if (line.playerResponses.Count > 0)
        {
            waitingForPlayerChoices = true; // Player must press space to see choices
        }

        // Debug action
        if (line.action != null)
        {
          

            if (line.action.GetPersistentEventCount() > 0)
            {
               
                line.action.Invoke();
            }
           
        }
    }


    public void NextLine()
    {
        if (currentDialogue == null) return;

        if (currentLineIndex < currentDialogue.dialoglines.Count - 1)
        {
            currentLineIndex++;
            ShowDialog();
        }
        else
        {
            EndDialogue();
        }
    }
    private void ShowPlayerResponses()
    {
        var line = currentDialogue.dialoglines[currentLineIndex];

        if (line.playerResponses.Count > 0)
        {
            dialogText.text = ""; 
            responseText.text = "Responses:\n";
            for (int i = 0; i < line.playerResponses.Count; i++)
            {
                responseText.text += $"{i + 1}. {line.playerResponses[i]}\n";
            }
        }
    }

    private void HandleResponse(int responseIndex)
    {
        if (currentDialogue == null) return;

        var line = currentDialogue.dialoglines[currentLineIndex];

        if (responseIndex < line.playerResponses.Count)
        {
            if (line.npcReactions.Count > responseIndex)
            {
                dialogText.text = line.npcReactions[responseIndex]; 
                responseText.text = ""; 
                waitingForReaction = true;
            }
        }
    }

    public void EndDialogue()
    {
        if (dialogueEnded) return;
        dialogPanel.SetActive(false);  
        currentDialogue = null;
        currentNPCID = null;
        if (PlayerStats.Instance != null && PlayerStats.Instance.level >= 3)
        {
            endGamePanel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else 
        {
            return;
        }

        dialogueEnded = true;

    }
    public void SetCondition(string key, bool value)
    {
        if (dialogConditions.ContainsKey(key))
        {
            dialogConditions[key] = value;
        }
        else
        {
            dialogConditions.Add(key, value);
        }
       
    }

}
