using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class PauseButton : ButtonBase
{
    private bool _paused;

    protected override void OnClicked()
    {
        base.OnClicked();
        _paused = !_paused;
        Time.timeScale = _paused ? 0f : 1f;
    }
}
