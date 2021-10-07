using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Levels Collection",menuName = "TowerDefense/Data Holders/Levels Collection")]
public class LevelsCollection : ScriptableObject
{
    [SerializeField]
    private List<LevelData> levelDatas = new List<LevelData>();

    public List<LevelData> LevelDatas { get => levelDatas; set => levelDatas = value; }

    public LevelData NextLevel(LevelData currentLevel)
    {
        int levelIndex = levelDatas.FindIndex(x => x == currentLevel);
        levelIndex = (int)Mathf.Repeat(levelIndex+1, levelDatas.Count);
        return levelDatas[levelIndex];
    }
}
