using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewLevelCompleted : MonoBehaviour
{
    [SerializeField]
    private HordeController hordeController;
    [SerializeField]
    private GameObject screenContent;
    [SerializeField]
    private ViewLevels viewLevels;
    [SerializeField]
    private LevelLoader levelLoader;

    public void Start()
    {
        hordeController.LevelCompleted += OnLevelCompleted;
    }
    void OnLevelCompleted()
    {
        screenContent.SetActive(true);
    }

    /// <summary>
    /// Invoked from the inspector
    /// </summary>
    public void OnNextLevel()
    {
        screenContent.SetActive(false);
        levelLoader.LoadNextLevel();
    }
    /// <summary>
    /// Invoked from the inspector
    /// </summary>
    public void OnOpenLevels()
    {
        screenContent.SetActive(false);
        viewLevels.Show();
    }
}
