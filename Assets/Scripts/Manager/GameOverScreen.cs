using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public GameObject gameOverScreen;

    [SerializeField] private AudioClip gameOverSFX;

    private void Start()
    {
        gameOverScreen.SetActive(false);
    }

    private void Update()
    {
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

    public void GameOver()
    {
        StartCoroutine(GameOverSequence());
    }

    IEnumerator GameOverSequence()
    {
        // Wait for 4 seconds
        yield return new WaitForSeconds(4f);

        // Execute these statements after the wait
        gameOverScreen.SetActive(true);
        SoundFXManager.Instance.PlaySoundFXClip(gameOverSFX, transform, 1f, "SFX");
    }
}
