using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoatMB : AnimalMB
{
    
    void Start()
    {
        animalName = "Goat";
        animalSpecies = "Ankara keçisi";
        LegCount = 4;
        hasWings = false;
        isCarnivorous = false;
        isMammal = true;
    }
    public override void DigestiveSystem()
    {
        base.DigestiveSystem();
        Debug.Log("I like eating clover");
    }
}
