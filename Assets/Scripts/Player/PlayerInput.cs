using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    InputAction move;

    private void Start()
    {
        move = InputSystem.actions.FindAction("Move");
    }

    public InputPackage GetInput()
    {
        InputPackage package = new InputPackage();
        package.movedir = move.ReadValue<Vector2>();
        return package;
    }
}
