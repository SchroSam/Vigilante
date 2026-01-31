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
        behavior.Process(input);
    }
}