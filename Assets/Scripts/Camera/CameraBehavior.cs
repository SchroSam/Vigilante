using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class CameraBehavior : MonoBehaviour
{
    public Transform player;
    InputAction look, changeLockon, debugKill;
    public static Transform lockOn;

    Vector3 desiredPosition;

    public float rotateSpeed = 180f;

    public static List<Transform> lockables = new List<Transform>();
    public static int iter = 0;

    private void Start()
    {
        look = InputSystem.actions.FindAction("Look");
        changeLockon = InputSystem.actions.FindAction("ChangeLockon");
        debugKill = InputSystem.actions.FindAction("DebugKill");
    }

    void Update()
    {
        desiredPosition = player.position;

        if(player.GetComponent<Player>().currentGroup == null)
        {
            Vector2 l = look.ReadValue<Vector2>() * rotateSpeed;
            float rotX = transform.localEulerAngles.y + l.x * Time.deltaTime;
            float rotY = transform.localEulerAngles.x + l.y * Time.deltaTime;
            if (rotY > 180f) rotY = Mathf.Clamp(rotY, 360f - 45f, 360f);
            else rotY = Mathf.Clamp(rotY, -10f, 45f);
            transform.rotation = Quaternion.Euler(rotY, rotX, 0f);

            // Over-the-shoulder offset (adjust values to taste)
            Vector3 shoulderOffset = transform.rotation * new Vector3(0.4f, 1.4f, -1.2f);
            desiredPosition = player.position + shoulderOffset;
        }

        else if (lockOn == null)
        {
            if (lockables.Count > 0)
            {
                lockOn = lockables[0];
                iter = 0;
            }
            Vector2 l = look.ReadValue<Vector2>() * rotateSpeed;
            float rotX = transform.localEulerAngles.y + l.x * Time.deltaTime;
            float rotY = transform.localEulerAngles.x + l.y * Time.deltaTime;
            if (rotY > 180f) rotY = Mathf.Clamp(rotY, 360f - 45f, 360f);
            else rotY = Mathf.Clamp(rotY, -10f, 45f);
            transform.rotation = Quaternion.Euler(rotY, rotX, 0f);
            desiredPosition = player.position + transform.rotation * new Vector3(0f, 1.4f, -1.2f);
        } else
        {
            if (changeLockon.WasPressedThisFrame())
            {
                iter += 1;
                iter %= lockables.Count;
                lockOn = lockables[iter];
            }
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lockOn.position - player.position) * Quaternion.Euler(30, 0, 0), Time.deltaTime * 1000f);

            if (debugKill.WasPressedThisFrame())
            {
                RemoveFromLockables(lockOn);
            }
        }
        transform.position = desiredPosition;
    }

    public static void RemoveFromLockables(Transform t)
    {
        if (!lockables.Contains(t)) return;
        bool refresh = lockables[iter] == t;
        lockables.Remove(t);
        if (refresh)
        {
            if (lockables.Count == 0)
            {
                lockOn = null;
                return;
            }
            iter %= lockables.Count;
            lockOn = lockables[iter];
        }
    }
}
