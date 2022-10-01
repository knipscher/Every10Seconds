using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private SceneLoader sceneLoader;
    private bool isRandomized = false;

    private float score;

    [SerializeField] private int minigameLength;

    private IEnumerator Start()
    {
        ChangeChannel();
        while (true)
        {
            yield return new WaitForSecondsRealtime(1);
            score++;
            UiManager.instance.SetScore(score);

            if (score % minigameLength == 0)
            {
                ChangeChannel();
            }
        }
    }

    private void ChangeChannel()
    {
        if (isRandomized)
        {
            sceneLoader.PlayRandomMinigame();
        }
        else
        {
            sceneLoader.PlayNextMinigame();
        }
    }
}
