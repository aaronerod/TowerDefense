using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewLevels : MonoBehaviour
{
    [SerializeField]
    private LevelsCollection levelsCollection;
    [SerializeField]
    private GameObject screenContent;
    [SerializeField]
    private LevelLoader levelLoader;

    [SerializeField]
    private Transform levelsParent;
    [SerializeField]
    private GameObject levelPrefab;
    private void Start()
    {
        LoadLevels();
    }

    void LoadLevels()
    {
        for(int i=0;i<levelsCollection.LevelDatas.Count;i++)
        {
            GameObject instance = Instantiate(levelPrefab, levelsParent, false);
            ViewLevel viewLevel = instance.GetComponent<ViewLevel>();
            viewLevel.Initialize(this, i, levelsCollection.LevelDatas[i]);

        }
    }

    public void Show()
    {
        screenContent.SetActive(true);
    }

    public void SelectLevel(LevelData levelData)
    {
        screenContent.SetActive(false);
        levelLoader.LoadLevel(levelData);
    }
}
