using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject playerObj;
    private Camera cam;

    [Header("UI")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI waveText;

    private int score = 0;
    private int highScore = 0;
    public int waveCount = 0;

    public bool isGameOver = false;

    public GameObject gameOverUI;
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI highScoreText;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            PlayerPrefs.DeleteKey("HighScore");

            highScore = PlayerPrefs.GetInt("HighScore", 0);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        cam = Camera.main;

        SetPlayerXPosition();
        UpdateScoreUI();
        UpdateWaveUI();

        gameOverUI.SetActive(false);
    }

    void Update()
    {
        SetPlayerXPosition();

        if (isGameOver)
        {
            GameOver();
        }

        if(isGameOver && Input.GetKeyDown(KeyCode.Space))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        }

        if (isGameOver) return;

        if(Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        }
    }

    void SetPlayerXPosition()
    {
        if (playerObj == null || cam == null) return;

        float currentY = playerObj.transform.position.y;
        float currentZ = playerObj.transform.position.z;

        Vector3 screenPos = new Vector3(
            Screen.width / 4f,
            Screen.height / 2f,
            cam.WorldToScreenPoint(playerObj.transform.position).z
        );

        Vector3 worldPos = cam.ScreenToWorldPoint(screenPos);

        playerObj.transform.position = new Vector3(worldPos.x, currentY, currentZ);
    }

    public void Score()
    {
        if (isGameOver) return;

        score += 5;

        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
        }

        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = score.ToString("000");
    }

    public void NextWave()
    {
        if (isGameOver) return;

        waveCount++;
        UpdateWaveUI();
    }

    void UpdateWaveUI()
    {
        if (waveText != null)
            waveText.text = waveCount.ToString("00");
    }

    public void GameOver()
    {
        isGameOver = true;
        if (gameOverUI != null)
            gameOverUI.SetActive(true);
        if (finalScoreText != null)
            finalScoreText.text = "SCORE: " + score.ToString("00");
        if (highScoreText != null)
            highScoreText.text = "HIGH SCORE: " + highScore.ToString("00");
    }

}