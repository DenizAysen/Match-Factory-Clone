using UnityEngine;

public class Sheep : Animal
{
    public override void DigestiveSystem()
    {
        base.DigestiveSystem();
        Debug.Log("I like eating grass");
    }
}
