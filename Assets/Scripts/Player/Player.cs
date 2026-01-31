using UnityEngine;

public class Player : MonoBehaviour
{
    public CharacterBehavior behavior;
    public PlayerInput pinput;
    InputPackage input;

    private void Update()
    {
        input = pinput.GetInput();
    }

    void FixedUpdate()
    {
        if (input.debugDie) behavior.forcedAct = "Die";
        if (input.debugReset) behavior.Reset();
        behavior.Process(input);
    }
}