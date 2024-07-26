using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Level/Data", fileName = "New Level Data")]
public class LevelDataSo : ScriptableObject
{
    public LevelData[] levelData;
}

[Serializable]
public struct LevelData
{
    public CollectableDataSo collectableDataSo;
    public int count;
}
