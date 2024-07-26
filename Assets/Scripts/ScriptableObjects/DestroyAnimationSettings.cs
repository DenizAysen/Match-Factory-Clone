using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Collectable/Destroy Animation Settings")]
public class DestroyAnimationSettings : ScriptableObject
{
    public GameObject ParticlePrefab;
    public Vector3 AnimationOffset;
    public float OffsetMoveDuration = .5f;
    public float MidMoveDuration = .35f;
}
