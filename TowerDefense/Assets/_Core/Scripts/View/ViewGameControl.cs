using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewGameControl : MonoBehaviour
{
    [SerializeField]
     private HordeController hordeController;
    [SerializeField]
    private LevelLoader levelLoader;
    [SerializeField]
    private GameObject buttonPlay;
    [SerializeField]
    private GameObject buttonStop;
    private void Start()
    {
        levelLoader.LevelLoaded += OnLevelLoaded;
        buttonPlay.SetActive(true);
        buttonStop.SetActive(false);
    }
    void OnLevelLoaded()
    {
        buttonPlay.SetActive(true);
        buttonStop.SetActive(false);
    }
    /// <summary>
    /// Called from the inspector
    /// </summary>
    public void OnPlay()
    {
        Time.timeScale = 1;
        if (!hordeController.IsPlaying)
            hordeController.StartHorde();
        buttonPlay.SetActive(false);
        buttonStop.SetActive(true);
    }
    /// <summary>
    /// Called from the inspector
    /// </summary>
    public void OnStop()
    {
        buttonPlay.SetActive(true);
        buttonStop.SetActive(false);

        levelLoader.Restart();
    }
}
