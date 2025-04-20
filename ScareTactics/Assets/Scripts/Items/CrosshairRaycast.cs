using UnityEngine;
using UnityEngine.UI;

public class CrosshairRaycast : MonoBehaviour
{
    public Image crosshairImage;
    public Color defaultColor = Color.white;
    public Color itemHoverColor = Color.green;
    public Color npcHoverColor = Color.yellow;
    public Color blockedColor = Color.red; 

    private ItemSlot itemSlot;
    private PlayerStats playerStats;

    private void Start()
    {
        itemSlot = FindObjectOfType<ItemSlot>();
        playerStats = FindObjectOfType<PlayerStats>();
    }

    private void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.E) && itemSlot != null && !itemSlot.IsEmpty)
        {
            itemSlot.ThrowItem(10f);
            return; 
        }

        crosshairImage.color = defaultColor;

        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, 3f))
        {
            ItemPickup itemPickup = hit.collider.GetComponent<ItemPickup>();
            NPCDialogueTrigger npcTrigger = hit.collider.GetComponent<NPCDialogueTrigger>();

            if (itemPickup != null)
            {
                bool canPickup = playerStats != null && playerStats.level >= itemPickup.requiredLevel;
                bool handIsEmpty = itemSlot != null && itemSlot.IsEmpty;

                if (canPickup && handIsEmpty)
                {
                    crosshairImage.color = itemHoverColor;

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        itemSlot.PlaceItem(itemPickup.itemPrefab);
                        Destroy(itemPickup.gameObject);
                    }

                    return;
                }
                else if (!handIsEmpty)
                {
                    crosshairImage.color = blockedColor;
                }

                return;
            }

            if (npcTrigger != null)
            {
                crosshairImage.color = npcHoverColor;

                if (Input.GetKeyDown(KeyCode.E))
                {
                    DialogueManager.Instance.StartDialogue(npcTrigger.dialogueData, npcTrigger.npcID);
                }

                return;
            }
        }
    }



}

