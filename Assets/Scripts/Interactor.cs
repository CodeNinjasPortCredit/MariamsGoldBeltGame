using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void Interact();
}

public class Interactor : MonoBehaviour
{
    public Transform InteractorSource;
    public float InteractRange;
    public LayerMask interactableLayer;

    public List<IInteractable> inventory = new List<IInteractable>();

    void Update()
    {
       //if (Input.GetKeyDown(KeyCode.E))
       //{
       //    CheckInteracte();
       //}
    }

    private void CheckInteracte()
    {
        Collider[] colliders = Physics.OverlapSphere(InteractorSource.position, InteractRange, interactableLayer);
        foreach (Collider collider in colliders)
        {
            Debug.Log(collider.gameObject.name);
            if (collider.TryGetComponent<IInteractable>(out var interactObj))
            {
                float distance = Vector3.Distance(InteractorSource.position, collider.transform.position);
                if (distance <= InteractRange && !inventory.Contains(interactObj))
                { 
                    interactObj.Interact();
                    inventory.Add(interactObj);
                }
            }
        }
    }

    // Color of the gizmo sphere
    public Color gizmoColor = Color.yellow;

    // Draws the gizmo sphere in the Scene view (always visible)
    private void OnDrawGizmos()
    {
        // Set the gizmo color
        Gizmos.color = gizmoColor;

        // Draw a solid sphere at the object's position
        Gizmos.DrawSphere(InteractorSource.position, InteractRange);

        // Optional: Draw a wireframe sphere for clarity
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(InteractorSource.position, InteractRange);
    }
}
