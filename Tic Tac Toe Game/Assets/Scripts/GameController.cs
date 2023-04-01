using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Text[] buttonList;
    private string playerSide;
    public GameObject gameOverPanel;
    public Text gameOverText;
    private int moveCount;
    public GameObject restartButton;

    public Text score;
    private int winsX;
    private int winsO;

    private int timeLeft = 5;
    public Text countdown;

    void Start()
    {
        StartCoroutine("LoseTime");
    }

    void Awake ()
    {       
        winsX = 0;
        winsO = 0;
        restartButton.SetActive(false);
        moveCount = 0;
        gameOverPanel.SetActive(false);
        playerSide = "X";
        SetGameControllerReferenceOnBottons();
    }

    void SetGameControllerReferenceOnBottons()
    {
        for(int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].GetComponentInParent<GridSpace>().SetGameControllerReference(this);
        }
    }
    
    public string GetPlayerSide()
    {
        if (playerSide == "X")
            return "X";
        else
            return "O";
    }

    void Update()
    {
        countdown.text = "Timer: " + timeLeft;
        score.text = "X: " + winsX + "   O: " + winsO;

        if (timeLeft <= 0)
        {
            gameOverPanel.SetActive(true);
            gameOverText.text = "It's A Draw";
            moveCount = 0;
            restartButton.SetActive(true);
        }

        if (timeLeft == -1)
        {
            timeLeft = 0;
        }      
    }

    public void EndTurn()
    {
        moveCount++;
        CheckForWin();
        ChanngesSides();
        if (moveCount == 9)
        {
            gameOverPanel.SetActive(true);
            gameOverText.text = "It's A Draw";
            moveCount = 0;
            restartButton.SetActive(true);
        }
    }

    private void CheckForWin()
    {
        if (buttonList[0].text == playerSide && buttonList[1].text == playerSide && buttonList[2].text == playerSide)
        {
            GameOver();
        }
        if (buttonList[3].text == playerSide && buttonList[4].text == playerSide && buttonList[5].text == playerSide)
        {
            GameOver();
        }
        if (buttonList[6].text == playerSide && buttonList[7].text == playerSide && buttonList[8].text == playerSide)
        {
            GameOver();
        }
        if (buttonList[0].text == playerSide && buttonList[3].text == playerSide && buttonList[6].text == playerSide)
        {
            GameOver();
        }
        if (buttonList[1].text == playerSide && buttonList[4].text == playerSide && buttonList[7].text == playerSide)
        {
            GameOver();
        }
        if (buttonList[2].text == playerSide && buttonList[5].text == playerSide && buttonList[8].text == playerSide)
        {
            GameOver();
        }
        if (buttonList[0].text == playerSide && buttonList[4].text == playerSide && buttonList[8].text == playerSide)
        {
            GameOver();
        }
        if (buttonList[2].text == playerSide && buttonList[4].text == playerSide && buttonList[6].text == playerSide)
        {
            GameOver();
        }
    }

    private void ChanngesSides()
    {
        timeLeft = 5;
        playerSide = (playerSide == "X")?"O":"X";
    }

    private void GameOver()
    {
        if (playerSide == "X")
            winsX++;
        else
            winsO++;
        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].GetComponentInParent<Button>().interactable = false;
            gameOverPanel.SetActive(true);
            gameOverText.text = playerSide + " Wins!!";
            restartButton.SetActive(true);
        }
        Time.timeScale = 0;
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        playerSide = "X";
        moveCount = 0;
        timeLeft = 5;
        gameOverPanel.SetActive(false);
        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].GetComponentInParent<Button>().interactable = true;
            buttonList[i].text = " ";
        }
        restartButton.SetActive(false);
    }

    IEnumerator LoseTime()
    {
        while(true)
        {
            yield return new WaitForSeconds(1);
            timeLeft--;
        }
    }
}
