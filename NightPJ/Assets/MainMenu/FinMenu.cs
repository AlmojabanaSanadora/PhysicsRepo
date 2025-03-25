using UnityEngine;
using UnityEngine.SceneManagement;

public class FinMenu : MonoBehaviour
{
    public GameObject gameOverUI; // Reference to the Game Over UI

    public void ShowGameOverUI()
    {
        Debug.Log("Game Over!");

        if (gameOverUI != null)
        {
            // Activate the Game Over UI
            gameOverUI.SetActive(true);

            // Pause the game
            Time.timeScale = 0;

            // Show and unlock the cursor
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            // Pause all audio sources
            AudioSource[] sonidos = FindObjectsOfType<AudioSource>();
            foreach (AudioSource audio in sonidos)
            {
                audio.Pause();
            }
        }
        else
        {
            Debug.LogWarning("Game Over UI is not set!");
        }
    }

    public void ResumeGame()
    {
        Debug.Log("Resuming Game...");

        if (gameOverUI != null)
        {
            // Deactivate the Game Over UI
            gameOverUI.SetActive(false);

            // Resume the game
            Time.timeScale = 1;

            // Hide and lock the cursor
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            // Resume all audio sources
            AudioSource[] sonidos = FindObjectsOfType<AudioSource>();
            foreach (AudioSource audio in sonidos)
            {
                audio.Play();
            }

            // Optionally reload the scene to reset the game state
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}