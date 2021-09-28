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
    public TMP_Text textAction;
    public TMP_Text textInstruction;
    public GameObject gameOverMenu;
    public GameObject mainMenu;
    public GameObject actionButtons;
    public GameObject commandMenu;
    public bool isTimer;
    
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
            AudioManager.Instance.playGameOver(true);
            AudioManager.Instance.playMusic(false);
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

    /// <summary>
    /// quit ui button quit is clicked
    /// </summary>
    public void btnQuit()
    {
        Debug.Log("Application Quitting...");
        Application.Quit();
    }

    /// <summary>
    /// attack ui button is clicked
    /// </summary>
    public void btnSelect()
    {
        Debug.Log("btnSelect...");
        GameManager.Instance.nextAction = GameManager.NextAction.Select;
        textAction.text = "Select";
    }
    
    /// <summary>
    /// attack ui button is clicked
    /// </summary>
    public void btnAttack()
    {
        Debug.Log("btnAttack...");
        GameManager.Instance.nextAction = GameManager.NextAction.Attack;
        textAction.text = "Attack";
    }
    
    /// <summary>
    /// move ui button is clicked
    /// </summary>
    public void btnMove()
    {
        Debug.Log("btnMove...");
        GameManager.Instance.nextAction = GameManager.NextAction.Move;
        textAction.text = "Move";
    }

    /// <summary>
    /// end turn ui button is clicked
    /// </summary>
    public void btnEndTurn()
    {
        Debug.Log("btnEndTurn...");
        GameManager.Instance.endTurn();
        textAction.text = "Select";
    }

    /// <summary>
    /// enables or disables the action buttons in menu
    /// </summary>
    /// <param name="yes">true or false</param>
    public void enableButtons(bool yes)
    {
        actionButtons.SetActive(yes);
        if (!yes)
        {
            textAction.text = "Select";
            GameManager.Instance.nextAction = GameManager.NextAction.Select;
        }
        
    }

    public void setTurnText(int teamID)
    {
        textInstruction.text = $"Player {teamID}'s Turn";
    }

    public void toggleCommandMenu(bool yes)
    {
        commandMenu.SetActive(yes);
    }
}