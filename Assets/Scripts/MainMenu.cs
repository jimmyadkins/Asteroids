using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        // Load the game scene (replace "SampleScene" with your actual game scene name)
        SceneManager.LoadScene("Asteroids");
    }

    public void QuitGame()
    {
        // Quit the game
        Application.Quit();
    }
}
