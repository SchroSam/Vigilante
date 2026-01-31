using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class CameraBehavior : MonoBehaviour
{
    public Transform player;
    InputAction look, changeLockon, debugKill;
    public static Transform lockOn;

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
        if (lockOn == null)
        {
            if (lockables.Count > 0)
            {
                lockOn = lockables[0];
                iter = 0;
            }
            Vector2 l = look.ReadValue<Vector2>() * rotateSpeed;
            float rotX = transform.localEulerAngles.y + l.x * Time.deltaTime;
            float rotY = transform.localEulerAngles.x + l.y * Time.deltaTime;
            if (rotY > 180f) rotY = Mathf.Clamp(rotY, 360f - 80f, 360f);
            else rotY = Mathf.Clamp(rotY, -1f, 80f);
            transform.rotation = Quaternion.Euler(rotY, rotX, 0f);
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
        transform.position = player.position;
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
