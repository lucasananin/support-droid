using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // Referencias;
    [SerializeField] GameObject _pausePanel = null;
    [SerializeField] GameObject _gameOverPanel = null;

    // Valores;
    [SerializeField] bool _isPaused = false;
    [SerializeField] bool _canPause = true;

    // Personalizados;

    private void Update()
    {
        PauseGame();
    }

    private void PauseGame()
    {
        if (Input.GetButtonDown("Cancel") && _canPause)
        {
            _isPaused = !_isPaused;
            _pausePanel.SetActive(_isPaused);
            Time.timeScale = (_isPaused) ? 0f : 1f;
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        _canPause = false;
        _gameOverPanel.SetActive(true);
    }
}
