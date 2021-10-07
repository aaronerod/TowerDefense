using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ViewLevel : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI txtLevel;

    private LevelData level;
    private ViewLevels viewLevels;
    public void Initialize(ViewLevels viewLevels, int index, LevelData level)
    {
        txtLevel.text = index.ToString();
        this.level = level;
        this.viewLevels = viewLevels;
    }

    /// <summary>
    /// Invoked from editor
    /// </summary>
    public void OnLevelSelected()
    {
        viewLevels.SelectLevel(level);
    }
}
