using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement; // For loading scenes like the main menu

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;  // Assign the Pause Menu Canvas in the Inspector
    private bool isPaused = false;

    void Start()
    {
        pauseMenuUI.SetActive(false);  // Ensure the pause menu is hidden at the start
    }

    // Function to be called when the 'onPause' event happens
    public void OnPause(InputValue value)
    {

        Debug.Log("Pausing");
        if (isPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);  // Hide pause menu
        Time.timeScale = 1f;           // Resume time
        isPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);   // Show pause menu
        Time.timeScale = 0f;           // Freeze time
        isPaused = true;
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;           // Ensure time is normal when going back to main menu
        SceneManager.LoadScene("MainMenu"); // Make sure to replace this with your main menu scene
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
