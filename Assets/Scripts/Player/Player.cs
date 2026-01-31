using UnityEngine;

public class Player : MonoBehaviour
{
    public CharacterBehavior model;
    public PlayerInput pinput;
    void FixedUpdate()
    {
        InputPackage input = pinput.GetInput();
        model.Process(input);
    }
}