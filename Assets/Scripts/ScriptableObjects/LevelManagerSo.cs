using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Level/Manager", fileName = "New Level Manager")]
public class LevelManagerSo : ScriptableObject
{
    public LevelDataSo[] Levels;

    public LevelDataSo GetCurrentLevelData()
    {
        int currentLevelDataIndex = PlayerPrefs.GetInt("Level");

        return Levels[(currentLevelDataIndex % Levels.Length)];
    }
}