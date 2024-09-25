using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public TMP_Text scoreText;  // Reference to the UI Text that displays the score
    private int score = 0;  // Current score

    public int largeAsteroidPoints = 10;
    public int mediumAsteroidPoints = 20;
    public int smallAsteroidPoints = 30;

    void Awake()
    {
        // Make sure only one instance of ScoreManager exists
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateScoreText();
    }

    // Method to add points to the score
    public void AddScore(int points)
    {
        Debug.Log("Adding Score");
        score += points;
        UpdateScoreText();
    }

    // Update the UI Text with the new score
    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    // Method to reset the score
    public void ResetScore()
    {
        score = 0;
        UpdateScoreText();
    }
}
