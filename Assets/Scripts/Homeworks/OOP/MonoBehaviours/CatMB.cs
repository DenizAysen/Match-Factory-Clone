using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatMB : AnimalMB
{
    private void Start()
    {
        animalName = "Cat";
        animalSpecies = "American Shorthair";
        LegCount = 4;
        hasWings = false;
        isCarnivorous = true;
        isMammal = true;
    }
    public override void DigestiveSystem()
    {
        base.DigestiveSystem();
        Debug.Log("I like eat meat");
    }
}
