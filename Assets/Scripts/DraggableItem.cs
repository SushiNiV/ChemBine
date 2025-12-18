using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform sandboxArea;
    public Transform trashCan;
    private GameObject activeObject;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (sandboxArea == null) sandboxArea = GameObject.Find("OpenArea").transform;
        if (trashCan == null) trashCan = GameObject.Find("Delete").transform;

        Debug.Log("=== OnBeginDrag START ===");
        Debug.Log($"This GameObject: {gameObject.name} (ID: {gameObject.GetInstanceID()})");
        Debug.Log($"This Parent: {(transform.parent != null ? transform.parent.name : "NULL")}");
        Debug.Log($"SandboxArea: {(sandboxArea != null ? sandboxArea.name : "NULL")}");
        Debug.Log($"Parent == SandboxArea? {transform.parent == sandboxArea}");
        Debug.Log($"Parent reference: {(transform.parent != null ? transform.parent.GetInstanceID().ToString() : "NULL")}");
        Debug.Log($"SandboxArea reference: {(sandboxArea != null ? sandboxArea.GetInstanceID().ToString() : "NULL")}");

        if (transform.parent == sandboxArea)
        {
            activeObject = gameObject;
            Debug.Log(">>> Using EXISTING object - NO clone created");
        }
        else
        {
            activeObject = Instantiate(gameObject, transform.root);
            activeObject.name = gameObject.name + "(Clone)";
            Debug.Log($">>> Creating NEW CLONE (ID: {activeObject.GetInstanceID()})");
        }

        Debug.Log($"ActiveObject: {activeObject.name} (ID: {activeObject.GetInstanceID()})");
        Debug.Log("=== OnBeginDrag END ===");

        activeObject.transform.SetParent(transform.root);
        CanvasGroup group = activeObject.GetComponent<CanvasGroup>();
        if (group == null) group = activeObject.AddComponent<CanvasGroup>();
        group.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (activeObject != null)
        {
            activeObject.transform.position = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (activeObject == null) return;
        activeObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        bool deleted = false;
        foreach (RaycastResult result in results)
        {
            if (trashCan != null && (result.gameObject.transform == trashCan || result.gameObject.transform.IsChildOf(trashCan)))
            {
                deleted = true;
                break;
            }
        }
        if (deleted)
        {
            Debug.Log($"Deleting: {activeObject.name}");
            Destroy(activeObject);
        }
        else
        {
            Debug.Log($"Placing in sandbox: {activeObject.name}");
            activeObject.transform.SetParent(sandboxArea);
        }
    }
}