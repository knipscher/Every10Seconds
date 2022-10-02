using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;

    [SerializeField] private TextMeshProUGUI scoreLabel;
    [SerializeField] private TextMeshProUGUI highScoreLabel;

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

    private void Start()
    {
        GameManager.instance.OnSetScore += SetScore;
        SetScore(0);
    }

    public void SetScore(int score)
    {
        scoreLabel.text = "SCORE: " + score;
        var highScore = PlayerPrefs.GetInt("HighScore");

        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
        }
        highScoreLabel.text = "HIGH SCORE: " + highScore;
    }
}