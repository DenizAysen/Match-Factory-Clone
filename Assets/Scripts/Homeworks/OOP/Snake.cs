using UnityEngine;

public class Snake : Animal
{
    public override void DigestiveSystem()
    {
        base.DigestiveSystem();
        Debug.Log("I like eating frogs");
    }
    public bool IsPoisonous {  get; set; }
}
