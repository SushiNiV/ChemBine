using UnityEngine;

public class ElementCombiner : MonoBehaviour
{
    public string partnerName;
    public GameObject resultPrefab;
    public Transform sandboxArea;
    private bool hasCombined = false; // Flag to prevent double combination
    
    private void Awake()
    {
        if (sandboxArea == null)
        {
            GameObject sa = GameObject.Find("OpenArea");
            if (sa != null) sandboxArea = sa.transform;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasCombined) return; // Already combined, ignore

        if (other.gameObject.name.Contains(partnerName))
        {
            // Check if the other object has also combined
            ElementCombiner otherCombiner = other.GetComponent<ElementCombiner>();
            if (otherCombiner != null && otherCombiner.hasCombined) return;

            Combine(other.gameObject);
        }
    }

    private void Combine(GameObject otherObject)
    {
        hasCombined = true;

        // Mark the other object as combined too
        ElementCombiner otherCombiner = otherObject.GetComponent<ElementCombiner>();
        if (otherCombiner != null) otherCombiner.hasCombined = true;

        Vector3 spawnPos = (transform.position + otherObject.transform.position) / 2;
        GameObject newElement = Instantiate(resultPrefab, spawnPos, Quaternion.identity, sandboxArea);
        newElement.name = resultPrefab.name + "(Clone)";

        // Set the sandboxArea reference on the new element
        DraggableItem draggable = newElement.GetComponent<DraggableItem>();
        if (draggable != null)
        {
            draggable.sandboxArea = sandboxArea;
        }

        Destroy(otherObject);
        Destroy(gameObject);
    }
}