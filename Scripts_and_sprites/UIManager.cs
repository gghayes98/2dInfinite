using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TMP_Text distanceText;
    public static UIManager Instance;
    public TMP_Text finalDistText;
    public GameObject gameOverPanel;
    public GameObject menuButtons;

    

    void Awake()
    {
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

    public void StartGame()
    {
        distanceText.text = "Distance: 0";
        menuButtons.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        distanceText.text = $"Distance: {(int) gameManager.Instance.distance}";
    }

    public void GameOver(float finalDist)
    {
        gameOverPanel.SetActive(true);
        finalDistText.text = $"Final Distance:\n{(int)finalDist}";
    }
}
