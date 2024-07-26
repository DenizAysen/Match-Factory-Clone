using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelButton : ButtonBase
{
    protected override void OnClicked()
    {
        base.OnClicked();
        int currentLevelDataIndex = PlayerPrefs.GetInt("Level");
        PlayerPrefs.SetInt("Level", currentLevelDataIndex + 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
