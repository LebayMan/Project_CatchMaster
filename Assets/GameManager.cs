using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int score = 0;
    public TextMeshProUGUI scoreText;
    public SerialController serialController;
    public GameObject losepanel;

    private void Awake()
    {
        Instance = this;
    }

    public void AddScore(int value)
    {
        score += value;
        scoreText.text = "Score: " + score;
        if (score < 0)
        {
            serialController.LoseScore();
            Time.timeScale = 0f;
            losepanel.SetActive(true);
        }
    }
    public void ResetScore()
    {
        Time.timeScale = 1f; // Reset time scale to normal
        losepanel.SetActive(false);
        score = 0;
        serialController.SendSerialMessage("reset");
        scoreText.text = "Score: " + score;
    }
}
