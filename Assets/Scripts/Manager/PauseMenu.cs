using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [Header("SFX")]
    [SerializeField] private AudioClip pauseSFX;

    public GameObject pauseMenu;
    public bool isPaused;
    private void Start()
    {
        pauseMenu.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                StartCoroutine(PauseGame());
            }
        }
    }

    private IEnumerator PauseGame()
    {
        if (!isPaused)
        {
            // Play pause SFX
            SoundFXManager.Instance.PlaySoundFXClip(pauseSFX, transform, 1f, "SFX");

            // Wait for the sound effect to finish playing
            yield return new WaitForSecondsRealtime(pauseSFX.length);

            // Pause background music and game
            SoundFXManager.Instance.PauseBackgroundMusic();
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
            isPaused = true;
        }
    }

    public void ResumeGame()
    {
        SoundFXManager.Instance.ResumeBackgroundMusic();
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void RestartLevel()
    {
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        // Resume the game time scale
        Time.timeScale = 1f;
    }
}
