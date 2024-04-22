using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Timeline.TimelinePlaybackControls;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    
    public int CurrentLevel = 1;
    public int TotalCoins = 0;
    public TextMeshProUGUI coinText;
    public ChestController chestController;


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
            return;
        }

        CurrentLevel++;
        SceneManager.LoadScene(CurrentLevel);
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
}
