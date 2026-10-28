using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int lives = 3;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI messageText;

    int score;
    int bricksRemaining;
    public bool IsGameOver { get; private set; }

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        bricksRemaining = FindObjectsByType<Brick>(FindObjectsSortMode.None).Length;
        messageText.text = "";
        UpdateUI();
    }

    void Update()
    {
        if (IsGameOver && Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BrickDestroyed(int points)
    {
        score += points;
        bricksRemaining--;
        UpdateUI();

        if (bricksRemaining <= 0)
            EndGame("You Win! Press R to restart");
    }

    public void LoseLife()
    {
        lives--;
        UpdateUI();

        if (lives <= 0)
            EndGame("Game Over. Press R to restart");
    }

    void EndGame(string message)
    {
        IsGameOver = true;
        messageText.text = message;
        Time.timeScale = 1f;
    }

    void UpdateUI()
    {
        scoreText.text = "Score: " + score;
        livesText.text = "Lives: " + lives;
    }
}