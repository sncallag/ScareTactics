using UnityEngine;

public class DialogEventListener : MonoBehaviour
{
    public void ActivatePanel(GameObject panel)
    {
        if (panel != null)
        {
            panel.SetActive(true);
        }
    }
}
