using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    
    public int CurrentLevel = 1;
    public int TotalCoins = 0;
    public TextMeshProUGUI coinText;
    public ChestController chestController;
    private bool isGamePaused = false;
    public GameObject pauseMenuUI;

    private BackgroundMusic backgroundMusic;

    void Start()
    {
        backgroundMusic = BackgroundMusic.instance;
    }



    public static GameManager Instance
    {
        get
        {
            if(m_Instance == null)
            {
                m_Instance = (GameManager) FindObjectOfType(typeof(GameManager));

                if(m_Instance == null)
                {
                    GameObject go = new GameObject();
                    m_Instance = go.AddComponent<GameManager>();
                }

                DontDestroyOnLoad(m_Instance.gameObject);
            }
            return m_Instance;
        }
    }
    private static GameManager m_Instance = null;


    public void StartGame()
    {
        Debug.Log("reset complete");
        CurrentLevel = 1;
        TotalCoins = 0;
        SceneManager.LoadScene(CurrentLevel);
        ResetUIText();
    }

    public void GoToNextLevel()
    {
        Debug.Log("go to next level");
        
        if (CurrentLevel == 4)
        {
            CurrentLevel = 1;
            SceneManager.LoadScene(0);
            backgroundMusic.PlayMusic();
            return;
        }

        CurrentLevel++;
        SceneManager.LoadScene(CurrentLevel);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGamePaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }
    public void PlayerReachedEndPoint()
    {
        GoToNextLevel();
    }
    public void CollectCoin()
    {
        TotalCoins++;
        Debug.Log("Total Coins: " + TotalCoins);
        UpdateCoinText();

        
    }
    public void UpdateCoinText()
    {
        if (coinText != null)
        {
            coinText.text = "Coins: " + TotalCoins;
        }
        else
        {
            Debug.LogWarning("Coin text reference is null. Ensure it is assigned in the Inspector.");
        }
    }
    public void ResetUIText()
    {
        TotalCoins = 0;
        UpdateCoinText();
        Debug.Log("UI Text Reset");
    }
    public void QuitGames()
    {
        Application.Quit();
        Debug.Log("Game is exiting");
        
    }

    public void PauseGame()
    {
        isGamePaused = true;
        Time.timeScale = 0f; 
        pauseMenuUI.SetActive(true); 
    }
    public void ResumeGame()
    {
        isGamePaused = false;
        Time.timeScale = 1f; 
        pauseMenuUI.SetActive(false); 
    }
}
