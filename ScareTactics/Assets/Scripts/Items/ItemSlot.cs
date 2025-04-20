using UnityEngine;

public class ItemSlot : MonoBehaviour
{
    public Transform attachPoint;
    private GameObject currentItemObject;

    public bool IsEmpty => currentItemObject == null;

    public void PlaceItem(GameObject itemPrefab)
    {
        if (!IsEmpty) return;

        // Get the original scale (world scale of prefab)
        Vector3 originalScale = itemPrefab.transform.localScale;

        // Instantiate without parent first
        GameObject newItem = Instantiate(itemPrefab, attachPoint.position, attachPoint.rotation);

        // Set its scale explicitly
        newItem.transform.localScale = originalScale;

        // Now parent it
        newItem.transform.SetParent(attachPoint, worldPositionStays: true);

        // Freeze physics
        Rigidbody rb = newItem.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }

        currentItemObject = newItem;
    }


    public void ClearSlot()
    {
        if (currentItemObject != null)
            Destroy(currentItemObject);

        currentItemObject = null;
    }

    public void ThrowItem(float throwForce)
    {
        if (IsEmpty) return;

        Rigidbody rb = currentItemObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Detach from hand
            currentItemObject.transform.parent = null;

            // Enable physics
            rb.isKinematic = false;

            // Add the ThrownItem marker
            if (!currentItemObject.GetComponent<ThrownItem>())
            {
                currentItemObject.AddComponent<ThrownItem>();
            }

            // Apply force forward
            rb.AddForce(transform.forward * throwForce, ForceMode.Impulse);
            AudioSource audio = GetComponentInChildren<AudioSource>();
            if (audio != null)
            {
                audio.Play();
            }
        }

        currentItemObject = null;
    }



}
