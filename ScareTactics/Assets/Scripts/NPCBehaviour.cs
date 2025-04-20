using UnityEngine;

public class NPCBehavior : MonoBehaviour
{
  
    public float reactionRadius = 5f;
    private Animator animator;
    public Material defaultFaceMaterial;
    public Material scaredFaceMaterial;
    public Renderer faceRenderer;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        if (animator == null)
        {
            Debug.LogWarning("Animator not found on NPC!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item") && other.GetComponent<ThrownItem>() != null)
        {
            Debug.Log($"{gameObject.name} detected a thrown item!");

            if (animator != null)
            {
                animator.SetTrigger("Scared");
                if (faceRenderer != null && scaredFaceMaterial != null)
                {
                    faceRenderer.material = scaredFaceMaterial;
                }
                Invoke("ResetFaceMaterial", 3.0f);
               
            }
            // Give XP to the player
            PlayerStats player = FindObjectOfType<PlayerStats>();
            if (player != null)
            {
                player.GainXP(10); // Adjust value to balance your game
            }

            Destroy(other.GetComponent<ThrownItem>()); // Prevent retrigger
        }

    }
    private void ResetFaceMaterial()
    {
        if (faceRenderer != null && defaultFaceMaterial != null)
        {
            faceRenderer.material = defaultFaceMaterial;
        }
    }




}
