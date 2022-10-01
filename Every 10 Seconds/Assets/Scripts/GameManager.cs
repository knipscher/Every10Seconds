using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    private bool isRandomized = false;

    private int timeSinceStart;
    private int score;

    [SerializeField] private int minigameLength;

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
        ChangeChannel();
        while (true)
        {
            yield return new WaitForSecondsRealtime(1);
            timeSinceStart++;

            if (timeSinceStart % minigameLength == 0)
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
}