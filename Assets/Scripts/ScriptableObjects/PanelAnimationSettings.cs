using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/UI/PanelAnimationSettings", fileName = "New Panel Settings")]
public class PanelAnimationSettings : ScriptableObject
{
    public float WinPanelActivationDelay = 1.6f;
    public float LosePanelActivationDelay = 1f;
}
