using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject playerObj;
    private Camera cam;

    [Header("UI")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI waveText;

    private int score = 0;
    public int waveCount = 0;

    public bool isGameOver = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
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
    }

    void Update()
    {
        SetPlayerXPosition();
    }

    void SetPlayerXPosition()
    {
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
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
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
        waveText.text = waveCount.ToString("00");
    }
}