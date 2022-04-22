using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public Color normalColor;
    public List<Color> colorList = new List<Color>();

    private Touch m_touch;
    private int m_touchCount;

    public static bool isGameStarted;

    private int m_score;
    public int Score
    {
        get
        {
            return m_score;
        }
        set
        {
            m_score = value;
        }
    }

    public Material platformMaterial;

    private int time;
    private int m_nextTime = 10;

    #region Data Variables
    private int m_highScore;
    private int m_playedGamesCount;
    #endregion

    [Header("Texts")]
    public Text touchCountText;
    public Text scoreText;
    public Text mainMenuHighScoreText;
    public Text gameOverHighScoreText;
    public Text gamesPlayedText;

    [Header("UI Objects")]
    public GameObject gameOverSceneUI;
    public GameObject inGameSceneUI;


    private void Awake()
    {
        Instance = this;
        if (!PlayerPrefs.HasKey("High Score")) {
            PlayerPrefs.SetInt("High Score", 0);
        }
        m_highScore = PlayerPrefs.GetInt("High Score");

        if (!PlayerPrefs.HasKey("Played Games Count")) {
            PlayerPrefs.SetInt("Played Games Count", 0);
        }
        m_playedGamesCount = PlayerPrefs.GetInt("Played Games Count");
    }
    private void Start()
    {
        platformMaterial.color = normalColor;
        mainMenuHighScoreText.text = $"BEST SCORE: {m_highScore}";
        gamesPlayedText.text = $"GAMES PLAYED: {m_playedGamesCount}";

        m_nextTime *= 60;
    }
    private void Update()
    {
        if (Input.touchCount > 0) {
            m_touch = Input.GetTouch(0);
        }
        if (m_touch.phase == TouchPhase.Began) {
            m_touchCount++;
            m_score++;
        }

        if (time > m_nextTime) {
            time = 0;
            int dice = Random.Range(0, colorList.Count);
            platformMaterial.color = colorList[dice];
        }

        touchCountText.text = $"{m_touchCount}";
    }
    private void FixedUpdate()
    {
        if (isGameStarted) { time++; }
    }

    public void GameOver()
    {
        gameOverHighScoreText.text = $"BEST SCORE {GetHighScore()}";
        scoreText.text = $"SCORE {m_score}";
        gameOverSceneUI.SetActive(true);
        inGameSceneUI.SetActive(false);
    }
    public int GetHighScore()
    {
        if (m_score > m_highScore) {
            PlayerPrefs.SetInt("High Score", m_score);
        }
        return PlayerPrefs.GetInt("High Score");
    }
}
