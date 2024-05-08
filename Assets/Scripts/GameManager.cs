using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool isGameover = false;

    public Text CurrentScoreText;
    public Text BestScoreText;
    public Text HealthText;
    public GameObject GameOverPanel;
    public float health = 5.0f;
    private int CurrentScore = 0;
    private int bestScore = 0;
    
    
    public int Score
    {
        get
        {
            return CurrentScore;
        }
        set
        {
            CurrentScore = value;
            CurrentScoreText.text = "현재 점수 : " + CurrentScore;
            if(CurrentScore >bestScore)
            {
                bestScore = CurrentScore;

                BestScoreText.text = "최고 점수 : " + bestScore;

                PlayerPrefs.SetInt("Best Score",bestScore);
            }
        }
    }
    public float Health
    {
        get
        {
            return health;
        }
        set
        {
            health = value;
            HealthText.text = "HEART : " + health;
        }
    }
    
    void Awake()
    {
        if(instance == null)
        {
            instance=this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void GameOver()
    {
        GameObject player = GameObject.Find("Player");
        if (health <= 0)
        {
            Destroy(player);
            isGameover = true;
            GameOverPanel.SetActive(true);
        }
    }
    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    void Start()
    {
        bestScore = PlayerPrefs.GetInt("Best Score", 0);
        BestScoreText.text = "최고 점수 : " + bestScore;
        CurrentScoreText.text = "현재 점수 : " + CurrentScore;
        HealthText.text = "HEART : " + health;
    }

    void Update()
    {
        GameOver();
    }
}
