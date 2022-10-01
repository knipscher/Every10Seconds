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
        losePanel.SetActive(false);
        ChangeChannel();

        while (!isGameOver)
        {
            yield return new WaitForSecondsRealtime(1);
            timeSinceStart++;
            Score(1);

            if (!isGameOver && timeSinceStart % minigameLength == 0)
            {
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
        if (isRandomized)
        {
            OnChangeRandomChannel?.Invoke();
        }
        else
        {
            OnChangeNextChannel?.Invoke();
        }
    }

    public void LoseGame()
    {
        losePanel.SetActive(true);
        isGameOver = true;
    }
}