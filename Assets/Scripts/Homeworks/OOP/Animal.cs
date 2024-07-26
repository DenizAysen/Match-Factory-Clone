using UnityEngine;
public class Animal 
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
    public bool HasWings { get; set; }
    public bool IsCarnivorous { get; set; }
    public bool IsMammal {  get; set; }
    public void ExcretorySystem() => Debug.Log(animalSpecies+ "'s excretory system works smoothly");
    public virtual void DigestiveSystem() => Debug.Log(animalSpecies + "'s digestive system works smoothly");
}
