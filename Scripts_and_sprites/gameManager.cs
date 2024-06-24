using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    public static gameManager Instance;
    public bool isGameActive;
    public float movementSpeed = 5f;
    public float distance = 0f;

    // Awake is called on startup
    void Awake()
    {
        // Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (isGameActive)
        {
            distance += movementSpeed / 100f;
            movementSpeed += 0.001f;
        }
    }

    public void StartGame()
    {
        StartCoroutine(Launch());
        // Reset any necessary stats
        distance = 0f;
        movementSpeed = 5f;
    }

    IEnumerator Launch()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Game");
        isGameActive = true;
        UIManager.Instance.StartGame();
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        isGameActive = false;
        float finalDist = distance;
        UIManager.Instance.GameOver(finalDist);
        // Trigger game over sequence
    }

    public void MainMenu()
    {
        Debug.Log("Menu!");
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
        UIManager.Instance.gameOverPanel.SetActive(false);
        UIManager.Instance.menuButtons.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
