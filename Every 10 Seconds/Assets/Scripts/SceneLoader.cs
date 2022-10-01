using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private enum Minigame { None, Fish, Dinosaur, Count }
    private Dictionary<Minigame, int> timesPlayedPerMinigame = new Dictionary<Minigame, int>();
    private Minigame currentMinigame;

    private void Awake()
    {
        for (int i = 0; i < (int)Minigame.Count; i++)
        {
            timesPlayedPerMinigame.Add((Minigame)i, 0);
        }
    }

    private void Start()
    {
        GameManager.instance.OnChangeNextChannel += PlayNextMinigame;
        GameManager.instance.OnChangeRandomChannel += PlayRandomMinigame;
    }

    private void OnDestroy()
    {
        GameManager.instance.OnChangeNextChannel -= PlayNextMinigame;
        GameManager.instance.OnChangeRandomChannel -= PlayRandomMinigame;
    }

    private void SelectMinigame(Minigame minigame)
    {
        if (currentMinigame != Minigame.None)
        {
            SceneManager.UnloadSceneAsync((int)currentMinigame);
        }

        currentMinigame = minigame;
        SceneManager.LoadScene((int) minigame, LoadSceneMode.Additive);
    }

    public void PlayNextMinigame()
    {
        var minigameIndex = (int)currentMinigame;
        minigameIndex++;
        if (minigameIndex == (int)Minigame.Count || minigameIndex == 0)
        {
            minigameIndex = 1; // Skip the TV room... although it might be kind of fun to have a TV room in there... recursively...
        }

        SelectMinigame((Minigame)minigameIndex);
    }

    public void PlayRandomMinigame()
    {
        var minigameIndex = Mathf.RoundToInt(Random.value * (int)Minigame.Count);
        SelectMinigame(currentMinigame);
    }
}
