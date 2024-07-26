using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OOP : MonoBehaviour
{
    private Animal[] Animals;
    [SerializeField] private AnimalMB[] AnimalMBs;
    void Start()
    {
        Animals = new Animal[3];

        Animal cat = new Cat();
        cat.animalName = "Cat";
        cat.animalSpecies = "American Shorthair";
        cat.LegCount = 4;
        cat.HasWings = false;
        cat.IsCarnivorous = true;
        cat.IsMammal = true;
        Animals[0] = cat;

        Animal sheep = new Sheep();
        sheep.animalName = "Sheep";
        sheep.animalSpecies = "Dorper";
        sheep.LegCount = 4;
        sheep.HasWings = false;
        sheep.IsCarnivorous = false;
        sheep.IsMammal = true;
        Animals[1] = sheep;

        Animal snake = new Snake();
        snake.animalName = "Snake";
        snake.animalSpecies = "Cobra";
        snake.LegCount = 0;
        snake.HasWings = false;
        snake.IsCarnivorous = true;
        snake.IsMammal = false;
        Animals[2] = snake;
        
    }
    private void PrintAnimalFeatures()
    {
        foreach (Animal animal in Animals) 
        {
            Debug.Log("Animal name : " + animal.animalName);
            Debug.Log("Animal species : " + animal.animalSpecies);
            Debug.Log("Animal leg count : " + animal.LegCount);
            Debug.Log("Has wings : " + animal.HasWings);
            Debug.Log("Is mammal : " + animal.IsMammal);
            animal.ExcretorySystem();
            animal.DigestiveSystem();
            Debug.Log("-----------------------------------");
        }
    }
    private void PrintAnimalMBFeatures()
    {
        foreach (AnimalMB animalMB in AnimalMBs)
        {
            Debug.Log("Animal name : " + animalMB.animalName);
            Debug.Log("Animal species : " + animalMB.animalSpecies);
            Debug.Log("Animal leg count : " + animalMB.LegCount);
            Debug.Log("Has wings : " + animalMB.hasWings);
            Debug.Log("Is mammal : " + animalMB.isMammal);
            animalMB.ExcretorySystem();
            animalMB.DigestiveSystem();
            Debug.Log("-----------------------------------");
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PrintAnimalFeatures();
        }
        else if (Input.GetKeyDown(KeyCode.T))
        {
            PrintAnimalMBFeatures();
        }
    }
}
