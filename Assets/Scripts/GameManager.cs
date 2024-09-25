using UnityEngine;
using UnityEngine.SceneManagement;  
using UnityEngine.UI;
using System.Collections;


public class GameManager : MonoBehaviour
{
    public GameObject gameOverUI;
    public float gameOverDelay = 2f;

    public void PlayerDied()
    {
        // Show the Game Over UI
        StartCoroutine(ShowGameOverUIWithDelay());
        //gameOverUI.SetActive(true);
        //Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        gameOverUI.SetActive(false);
        ScoreManager.instance.ResetScore();

        //Time.timeScale = 1f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator ShowGameOverUIWithDelay()
    {
        yield return new WaitForSeconds(gameOverDelay); 
        gameOverUI.SetActive(true);  
    }
}

