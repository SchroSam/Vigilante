using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    InputAction move;
    InputAction iHit, jHit, kHit, lHit;
    InputAction block;

    private void Start()
    {
        move = InputSystem.actions.FindAction("Move");
        block = InputSystem.actions.FindAction("Block");
    }

    public InputPackage GetInput()
    {
        InputPackage package = new InputPackage();
        package.movedir = move.ReadValue<Vector2>();
        if (package.movedir != Vector2.zero) {
            package.action = "Move";
        } else
        {
            package.action = "Idle";
        }
        bool blocking = block.IsPressed();
        print(blocking);
        return package;
    }
}
