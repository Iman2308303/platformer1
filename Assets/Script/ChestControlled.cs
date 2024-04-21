using UnityEngine;
using UnityEngine.UI;

public class ChestController : MonoBehaviour
{
    public int requiredCoins = 10;
    public Text displayText;
    public Animator animator;


    private GameManager gameManager;
    private bool isOpen = false;
 

    void Start()
    {
        gameManager = GameManager.Instance;

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isOpen)
        {
            displayText.gameObject.SetActive(true);
            UpdateDisplayText();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            displayText.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (isOpen || !displayText.gameObject.activeSelf)
            return;

        if (Input.GetKeyDown(KeyCode.E) && gameManager.TotalCoins >= requiredCoins)
        {
            OpenChest();
        }
    }

    void OpenChest()
    {
        if (animator != null)
        {
            isOpen = true;
            animator.SetTrigger("Open");
            
            gameManager.TotalCoins -= requiredCoins; 
            gameManager.UpdateCoinText(); 
        }
        else
        {
            Debug.LogWarning("Animator reference is null. Ensure it is assigned in the Inspector.");
        }
    }

    
    void UpdateDisplayText()
    {
        if (displayText != null)
        {
            displayText.text = gameManager.TotalCoins + "/" + requiredCoins + "Press 'E'";
        }
        else
        {
            Debug.LogWarning("Display text reference is null. Ensure it is assigned in the Inspector.");
        }
    }
}