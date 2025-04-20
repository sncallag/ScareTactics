using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "DialogueData", menuName = "Scriptable Objects/DialogueData")]
public class DialogueData : ScriptableObject
{
    [System.Serializable]
    public class DialogLine
    {
        public string lineText;
        public bool isConditional;
        public string conditionKey;
        public List<string> playerResponses;
        public List<string> npcReactions;
        public UnityEvent action;
    }

    public List<DialogLine> dialoglines;
}
