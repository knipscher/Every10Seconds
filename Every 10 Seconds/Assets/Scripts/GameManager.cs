using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int minigameLength;
    [SerializeField] private GameObject losePanel;

    private bool isRandomized = false;

    private int timeSinceStart;
    private int score;

    private bool isGameOver = false;

    public int level = 1;

    public Action OnChangeRandomChannel;
    public Action OnChangeNextChannel;

    public Action<int> OnSetScore;

    public int bridgerfinQuestionIndex { get; private set; } = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator Start()
    {
        bridgerfinQuestionIndex = PlayerPrefs.GetInt("QuestionIndex");
        losePanel.SetActive(false);
        ChangeChannel();
        
        while (!isGameOver)
        {
            yield return new WaitForSecondsRealtime(1);
            timeSinceStart++;
            UiManager.instance.SetCounter(minigameLength - timeSinceStart);
            Score(1);

            if (!isGameOver && timeSinceStart % minigameLength == 0)
            {
                timeSinceStart = 0;
                ChangeChannel();
            }
        }
    }

    public void Score(int points)
    {
        score += points;
        OnSetScore?.Invoke(score);
    }

    private void ChangeChannel()
    {
        OnChangeNextChannel?.Invoke();
        /*
        if (level > 1)
        {
            OnChangeRandomChannel?.Invoke();
        }
        else
        {
            OnChangeNextChannel?.Invoke();
        }
        */
    }

    public void LoseGame()
    {
        Debug.LogWarning("lost game!");
        losePanel.SetActive(true);
        isGameOver = true;
    }

    public void IncrementQuestionIndex()
    {
        bridgerfinQuestionIndex++;
        PlayerPrefs.SetInt("QuestionIndex", bridgerfinQuestionIndex);
    }

    public void ResetQuestionIndex()
    {
        bridgerfinQuestionIndex = 0;
        PlayerPrefs.SetInt("QuestionIndex", bridgerfinQuestionIndex);
    }
}