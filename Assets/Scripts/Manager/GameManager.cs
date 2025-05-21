using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private GameObject gameOverCanvas;
    [SerializeField] private GameObject victoryCanvas;
    [SerializeField] private GameObject startCanvas;


    private void Awake()
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

    private Button FindButton(Transform root, string targetName)
    {
        foreach (Transform child in root)
        {
            if (child.name == targetName)
            {
                Button button = child.GetComponent<Button>();
                if (button != null) return button;
            }

            Button foundInChild = FindButton(child, targetName);
            if (foundInChild != null)
            {
                return foundInChild;
            }
        }

        return null;

    }

    public void BindGameOverUI(GameObject canvas)
    {
        gameOverCanvas = canvas;

        Button restartButton = FindButton(canvas.transform, "RestartButton");
        Button mainMenuButton = FindButton(canvas.transform, "MainMenuButton");

        if (restartButton != null)
        {
            restartButton.onClick.RemoveAllListeners();
            restartButton.onClick.AddListener(RestartGame);
            //Debug.Log("[GameManager] Restart Button find!");
        }

        if (mainMenuButton != null)
        {
            mainMenuButton.onClick.RemoveAllListeners();
            mainMenuButton.onClick.AddListener(ReturnToMainMenu);
            //Debug.Log("[GameManager] Main Menu Button find!");
        }
        
        gameOverCanvas.SetActive(false);
    }

    public void BindVictoryUI(GameObject canvas)
    {
        victoryCanvas = canvas;
        victoryCanvas.SetActive(false);
        
        var restartBtn = FindButton(canvas.transform, "RestartButton");
        var menuBtn = FindButton(canvas.transform, "MainMenuButton");
        var quitBtn = FindButton(canvas.transform, "Quit");

        if (restartBtn != null)
        {
            restartBtn.onClick.RemoveAllListeners();
            restartBtn.onClick.AddListener(RestartGame);
        }

        if (menuBtn != null)
        {
            menuBtn.onClick.RemoveAllListeners();
            menuBtn.onClick.AddListener(ReturnToMainMenu);
        }
    }

    public void BindStartMenuUI(GameObject canvas)
    {
        startCanvas = canvas;

        var startBtn = FindButton(canvas.transform, "StartGame");
        var quitBtn = FindButton(canvas.transform, "Quit");
        
        startBtn.onClick.RemoveAllListeners();
        startBtn.onClick.AddListener(StartGame);
        
        quitBtn.onClick.RemoveAllListeners();
        quitBtn.onClick.AddListener(QuitGame);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GameOver()
    {
        gameOverCanvas.SetActive(true);
        Time.timeScale = 0;
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        gameOverCanvas.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ShowVictoryScreen()
    {
        Time.timeScale = 0;
        victoryCanvas.SetActive(true);
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenuScene");
    }
}
