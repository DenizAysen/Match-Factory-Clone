using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Collectable/Data", fileName = "New Collectable Data")]
public class CollectableDataSo : ScriptableObject
{
    public event Action OnReleaseEvent;

    public int id;
    public float jumpForce;
    public string collectableName;
    public Sprite sprite;
    public AudioDataSo mergeSFX;

    public void OnRelease() => OnReleaseEvent?.Invoke();
    public void ClearEvents() => OnReleaseEvent = null;
}
