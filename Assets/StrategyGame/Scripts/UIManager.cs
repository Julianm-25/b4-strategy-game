using System;
using System.Collections;
using TeddyToolKit.Core;
using TMPro;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{

    public int score = 0;
    public float timer = 60;
    public TMP_Text textScore;
    public TMP_Text textTimer;
    public GameObject gameOverMenu;
    public GameObject mainMenu;
    public bool isTimer;
    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private GameManager _gameManager;
    
    public void Start()
    {
        score = 0;
        timer = 60;
        addScore();
        showGameOver(false);
        showMainMenu(false);
        isTimer = false;
        AudioManager.Instance.playMusic(true);
        GameManager.Instance.startNewGame();
    }

    public void addScore()
    {
        score += 0;
        if (score < 1) score = 0;
        textScore.text = score.ToString();
    }

    public void startTimer()
    {
        if (isTimer)
        {
            if (timer >= 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                timer = 0;
                isTimer = false;
                showGameOver();
            }
            textTimer.text = Mathf.FloorToInt(timer).ToString();
        }
    }

    public void FixedUpdate()
    {
        //startTimer();
    }

    /// <summary>
    /// catches keypresses related for the UI, usually the main menu
    /// </summary>
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) toggleMainMenu();
    }

    public void showGameOver(bool show=true)
    {
        gameOverMenu.SetActive(show);
        isTimer = !show;
        if (show)
        {
            _audioManager.playGameOver(true);
            _audioManager.playMusic(false);
        }
    }

    public void showMainMenu(bool show=true)
    {
        mainMenu.SetActive(show);
        isTimer = !show;
    }

    public void toggleMainMenu()
    {
        mainMenu.SetActive(!mainMenu.activeSelf);
        isTimer = !mainMenu.activeSelf;
    }
    
    public bool isMenu()
    {
        return gameOverMenu.activeSelf || mainMenu.activeSelf;
    }

    public void btnQuit()
    {
        Debug.Log("Application Quitting...");
        Application.Quit();
    }
}