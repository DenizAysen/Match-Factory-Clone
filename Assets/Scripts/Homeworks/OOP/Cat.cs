using UnityEngine;
public class Cat : Animal
{
    public override void DigestiveSystem()
    {
        base.DigestiveSystem();
        Debug.Log("I like eating fish");
    }
}
