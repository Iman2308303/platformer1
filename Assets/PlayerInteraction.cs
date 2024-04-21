using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    public Text interactionText; // Reference to the UI Text element that displays interaction information
    public KeyCode interactKey = KeyCode.E; // The key used to interact with the chest
    private GameObject currentChest; // Reference to the current chest GameObject

    private bool isInRange = false; // Flag to track if the player is in range of the chest

    private void Update()
    {
        // Check if the player is in range of the chest and presses the interact key
        if (isInRange && Input.GetKeyDown(interactKey))
        {
            // If the player has enough coins, try to open the chest
            if (currentChest != null)
            {
                ChestController chestController = currentChest.GetComponent<ChestController>();
                // Add logic to interact with the chest
            }
        }
    }

    // Called when the player enters the trigger collider of a chest
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Chest"))
        {
            isInRange = true;
            currentChest = other.gameObject;
            interactionText.gameObject.SetActive(true);
            UpdateInteractionText();
        }
    }

    // Called when the player exits the trigger collider of a chest
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Chest"))
        {
            isInRange = false;
            currentChest = null;
            interactionText.gameObject.SetActive(false);
        }
    }

    // Update the interaction text to display the required key and instruction
    private void UpdateInteractionText()
    {
        Debug.Log("Updating interaction text.");
        interactionText.text = "Press " + interactKey.ToString() + " to interact";
    }
}