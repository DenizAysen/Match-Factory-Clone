using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdMB : AnimalMB
{
    void Start()
    {
        animalName = "Bird";
        animalSpecies = "Love Bird";
        LegCount = 2;
        hasWings = true;
        isCarnivorous = false;
        isMammal = false;
    }
    public override void DigestiveSystem()
    {
        base.DigestiveSystem();
        Debug.Log("I like eating carrot");
    }
}
