using UnityEngine;

public class ThrownItem : MonoBehaviour
{
    private void Start()
    {
        Destroy(this, 2f); // Auto-remove after 2 seconds
    }
}
