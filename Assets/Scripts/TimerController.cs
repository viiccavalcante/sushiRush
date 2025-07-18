using UnityEngine;
using TMPro;

public class TimerController : MonoBehaviour
{
    public float totalTime = 60f;
    private float currentTime;
    public TextMeshProUGUI timerText;
    public GameObject gameOverPanel;

    private bool isGameOver = false;

    void Start()
    {
        currentTime = totalTime;
        gameOverPanel.SetActive(false);
    }

    void Update()
    {
        if (isGameOver)
            return;

        currentTime -= Time.deltaTime;
        currentTime = Mathf.Max(currentTime, 0f);

        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);

        timerText.text = $"{minutes:00}:{seconds:00}";

        if (currentTime <= 0f)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        isGameOver = true;
        Time.timeScale = 0f; 
        gameOverPanel.SetActive(true);
    }
}
