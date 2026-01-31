using UnityEngine;
using UnityEngine.InputSystem;

public class CameraBehavior : MonoBehaviour
{
    public Transform player;
    InputAction look;

    public float rotateSpeed = 180f;

    private void Start()
    {
        look = InputSystem.actions.FindAction("Look");
    }

    void Update()
    {
        transform.position = player.position;
        Vector2 l = look.ReadValue<Vector2>() * rotateSpeed;
        float rotX = transform.localEulerAngles.y + l.x * Time.deltaTime;
        float rotY = transform.localEulerAngles.x + l.y * Time.deltaTime;
        if (rotY > 180f) rotY = Mathf.Clamp(rotY, 360f - 80f, 360f);
        else rotY = Mathf.Clamp(rotY, -1f, 80f);
        transform.rotation = Quaternion.Euler(rotY, rotX, 0f);
    }
}
