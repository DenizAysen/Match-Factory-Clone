using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsButton : ButtonBase
{
    private float delay = .2f;
    protected override void OnClicked()
    {
        base.OnClicked();
        StartCoroutine(LoadNewLevel());
    }

    private IEnumerator LoadNewLevel()
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
