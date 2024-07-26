using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AnimalMB : MonoBehaviour
{
    public string animalName;
    public string animalSpecies;
    public int LegCount
    {
        get => _legCount;
        set
        {
            if (value < 0)
            {
                Debug.LogWarning("Leg count cannot be negative");
                return;
            }

            _legCount = value;
        }
    }

    private int _legCount;

    public bool hasWings;

    public bool isCarnivorous;

    public bool isMammal;
    public void ExcretorySystem() => Debug.Log(animalSpecies + "'s excretory system works smoothly");
    public virtual void DigestiveSystem() => Debug.Log(animalSpecies + "'s digestive system works smoothly");
}
