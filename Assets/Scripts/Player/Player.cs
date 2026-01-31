using UnityEngine;

public class Player : MonoBehaviour
{
    public CharacterBehavior behavior;
    public PlayerInput pinput;

    void FixedUpdate()
    {
        InputPackage input = pinput.GetInput();
        behavior.Process(input);
    }
}