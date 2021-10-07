using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewGameOver : MonoBehaviour
{
    [SerializeField]
    private PlayerData playerData;
    [SerializeField]
    private LevelLoader levelLoader;
    [SerializeField]
    private GameObject gameOverContent;
    [SerializeField]
    private ViewLevels viewLevels;
    // Start is called before the first frame update
    void Start()
    {
        playerData.TowerDestoryed += OnTowerDestroyed;
        gameOverContent.SetActive(false);
    }
    void OnTowerDestroyed()
    {
        gameOverContent.SetActive(true);
    }

    /// <summary>
    /// Invoked from the inspector
    /// </summary>
    public void OnRestart()
    {
        levelLoader.Restart();
        gameOverContent.SetActive(false);
    }
    /// <summary>
    /// Invoked from the inspector
    /// </summary>
    public void OnMoreLevels()
    {
        gameOverContent.SetActive(false);
        viewLevels.Show();
    }
}
