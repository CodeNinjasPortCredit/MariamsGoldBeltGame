using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Interectable : MonoBehaviour
{
    public Transform head;

    [Header("Max Distance of the RayCast")]
    public int maxDistance;

    [Header("List of Layer Masks")]
    public LayerMask[] layerMask;

    [Header("Hint Message to Display")]
    public string hintMessage;

    [Header("Hint Text UI Element")]
    public TMPro.TextMeshProUGUI hintText;

    [Header("Reminder Text UI Element")]
    public TMPro.TextMeshProUGUI reminder;

    public float distance;

    [Header("Inventory Manager")]
    public InventoryManager inventory_manager;

    [Header("KeyPad UI")]
    public GameObject keypad;

    public float radius;

    // Update is called once per frame
    void Update()
    {
        if (transform.tag == "MainCamera")
        {
            Debug.DrawRay(head.position, transform.TransformDirection(Vector3.forward) * maxDistance, Color.red);
            RaycastHit hit;
            Vector3 rayOrigin = new Vector3(0.5f, 0.5f, 0f); // center of the screen
            float rayLength = distance;

            // actual Ray
            Ray ray = Camera.main.ViewportPointToRay(rayOrigin);

            // debug Ray
            Debug.DrawRay(ray.origin, ray.direction * rayLength, Color.red);

            if (Physics.SphereCast(head.position, radius, transform.TransformDirection(Vector3.forward), out hit, maxDistance, layerMask[0]))
            {
                // Debug.Log("Found an envelope!");
                reminder.text = "Press E to Open the Envelope";

                // Check if E is pressed down
                if (Input.GetKeyDown(KeyCode.E))
                {
                    //SDebug.Log("E pressed!");
                    //Debug.Log("Interecting with " + hit.collider.gameObject.name);
                    hintText.text = hit.collider.gameObject.GetComponent<Interectable>().hintMessage;
                }
            }
            else if (Physics.SphereCast(head.position, radius, transform.TransformDirection(Vector3.forward), out hit, maxDistance, layerMask[1]))
            {
                Debug.Log("Detected " + hit.collider.name);
                if (!inventory_manager.HasItem("Wrench"))
                {
                    reminder.text = "Find the Wrench to open the Bar.";
                }
                else
                {
                    reminder.text = "Press E to Interact with the Bar";
                }

                // Check if E is pressed down
                //if (Input.GetKeyDown(KeyCode.E))
                //{
                //    Debug.Log("You pressed E");
                //}
                //if (inventory_manager.HasItem("Wrench"))
                //{
                //    Debug.Log("You have Wrench");
                //}
                if (Input.GetKeyDown(KeyCode.E) && inventory_manager.HasItem("Wrench"))
                {
                    hit.collider.gameObject.GetComponent<Animator>().SetTrigger("OpenBar");
                }
            }
            else if (Physics.SphereCast(head.position, radius, transform.TransformDirection(Vector3.forward), out hit, maxDistance, layerMask[2]))
            {
                Debug.Log("Detected " + hit.collider.name);

                if (!inventory_manager.HasItem("Key") && hit.collider.gameObject.transform.parent.gameObject.name == "Door")
                {
                    reminder.text = "Find the Key to open the Door.";
                    keypad.SetActive(false);
                }
                if (Input.GetKeyDown(KeyCode.E) && inventory_manager.HasItem("BlackKey"))
                {
                    if (hit.collider.gameObject.transform.parent.gameObject.name == "Door1")
                    {
                        hit.collider.gameObject.transform.parent.gameObject.GetComponent<Animator>().SetBool("Open", true);
                        hit.collider.gameObject.layer = LayerMask.NameToLayer("Default");

                    }
                }
                if (Input.GetKeyDown(KeyCode.E) && inventory_manager.HasItem("SilverKey"))
                {
                    if (hit.collider.gameObject.transform.parent.gameObject.name == "Door2")
                    {
                        hit.collider.gameObject.transform.parent.gameObject.GetComponent<Animator>().SetBool("Open", true);
                        hit.collider.gameObject.layer = LayerMask.NameToLayer("Default");

                    }
                }
                else if (inventory_manager.HasItem("Key") && hit.collider.gameObject.transform.parent.gameObject.name == "Door")
                {
                    reminder.text = "Press E to open the Door.";
                }

                if (!inventory_manager.HasItem("BlackKey") && hit.collider.gameObject.transform.parent.gameObject.name == "Door1")
                {
                    reminder.text = "Find the Black Key to open the Door.";
                    keypad.SetActive(false);
                }
                else if (inventory_manager.HasItem("BlackKey") && hit.collider.gameObject.transform.parent.gameObject.name == "Door1")
                {
                    reminder.text = "Press E to open the Door.";
                }

                if (!inventory_manager.HasItem("SilverKey") && hit.collider.gameObject.transform.parent.gameObject.name == "Door2")
                {
                    reminder.text = "Find the Silver Key to open the Door.";
                    keypad.SetActive(false);
                }
                else if (inventory_manager.HasItem("SilverKey") && hit.collider.gameObject.transform.parent.gameObject.name == "Door2")
                {
                    reminder.text = "Press E to open the Door.";
                }

                if (hit.collider.gameObject.transform.parent.gameObject.name == "PasswordDoor")
                {
                    reminder.text = "Input the password to open the door";
                    keypad.SetActive(true);
                }
                else if (hit.collider.gameObject.transform.parent.gameObject.name != "PasswordDoor")
                {
                    keypad.SetActive(false);
                }

                // Check if E is pressed down
                //if (Input.GetKeyDown(KeyCode.E))
                //{
                //Debug.Log("You pressed E");
                //  }
                // if (inventory_manager.HasItem("Key"))
                //{
                //  Debug.Log("You have Key");
                // }



                if (Input.GetKeyDown(KeyCode.E) && inventory_manager.HasItem("Key"))
                {
                    if (hit.collider.gameObject.transform.parent.gameObject.name == "Door")
                    {
                        hit.collider.gameObject.transform.parent.gameObject.GetComponent<Animator>().SetBool("Open", true);
                        hit.collider.gameObject.layer = LayerMask.NameToLayer("Default");
                    }
                }
            }
            else if (Physics.SphereCast(head.position, radius, transform.TransformDirection(Vector3.forward), out hit, maxDistance, layerMask[3]))
            {
                Debug.Log("Detected " + hit.collider.name);
                if (hit.collider.tag == "Key")
                {
                    reminder.text = "Press E to pick up Key";

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        hit.collider.gameObject.GetComponent<IInteractable>().Interact();
                        gameObject.GetComponent<Interactor>().inventory.Add(hit.collider.gameObject.GetComponent<IInteractable>());

                    }
                }
                else if (hit.collider.tag == "Wrench")
                {
                    reminder.text = "Press E to pick up Wrench";

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        hit.collider.gameObject.GetComponent<IInteractable>().Interact();
                        gameObject.GetComponent<Interactor>().inventory.Add(hit.collider.gameObject.GetComponent<IInteractable>());

                    }
                }
            }
            else
            {
                reminder.text = "";
            }
        }
    }
}

