using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;

    [SerializeField] private TextMeshProUGUI scoreLabel;
    [SerializeField] private TextMeshProUGUI highScoreLabel;

    [SerializeField] private TextMeshProUGUI timeCounter;

    [SerializeField] private GameObject infoPanel;
    [SerializeField] private TextMeshProUGUI infoLabel;

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
        infoPanel.SetActive(false);
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

    public void SetInfoText(string info)
    {
        infoPanel.SetActive(true); 
        infoLabel.text = info;
        StartCoroutine(DelayThenDeactivateInfoText());
    }

    public void SetCounter(int timeLeft)
    {
        timeCounter.text = timeLeft.ToString();
    }

    private IEnumerator DelayThenDeactivateInfoText()
    {
        yield return new WaitForSeconds(3);
        infoPanel.SetActive(false);
    }
}