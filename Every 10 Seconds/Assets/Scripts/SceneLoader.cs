using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private enum Minigame
    {
        None,
        Fish,
        Dinosaur,
        Bridgerfin,
        Count
    }

    [SerializeField] private string[] minigameInfos;

    private Dictionary<Minigame, int> timesPlayedPerMinigame = new Dictionary<Minigame, int>();
    private Minigame currentMinigame;

    private void Awake()
    {
        for (int i = 0; i < (int) Minigame.Count; i++)
        {
            timesPlayedPerMinigame.Add((Minigame) i, 0);
        }
    }

    private void Start()
    {
        GameManager.instance.OnChangeNextChannel += PlayNextMinigame;
        GameManager.instance.OnChangeRandomChannel += PlayRandomMinigame;

        UiManager.instance.SetInfoText(minigameInfos[1]);
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
            SceneManager.UnloadSceneAsync((int) currentMinigame);
        }

        UiManager.instance.SetInfoText(minigameInfos[(int)minigame]);

        currentMinigame = minigame;
        SceneManager.LoadScene((int) minigame, LoadSceneMode.Additive);
    }

    public void PlayNextMinigame()
    {
        var minigameIndex = (int) currentMinigame;
        minigameIndex++;
        if (minigameIndex == (int) Minigame.Count || minigameIndex == 0)
        {
            GameManager.instance.level++; // Every time you do a full loop, increase the level
            minigameIndex = 1; // Skip the TV room... although it might be kind of fun to have a TV room in there... recursively...
        }

        SelectMinigame((Minigame) minigameIndex);
    }

    public void PlayRandomMinigame()
    {
        var minigameIndex = GetRandomMinigameIndex((int) currentMinigame);
        SelectMinigame((Minigame) minigameIndex);
    }

    private int GetRandomMinigameIndex(int currentMinigameIndex)
    {
        var minigameIndex = Mathf.RoundToInt(Random.value * (int) Minigame.Count - 1);
        if (minigameIndex > 0 && minigameIndex != currentMinigameIndex)
        {
            return minigameIndex;
        }
        else
        {
            return GetRandomMinigameIndex(currentMinigameIndex);
        }
    }

    public void ResetGame()
    {
        Debug.LogWarning("reloading!");
        SceneManager.LoadScene(0);
    }
}
