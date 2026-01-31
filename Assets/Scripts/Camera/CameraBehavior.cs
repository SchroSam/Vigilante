using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    public Transform player;
    
    void Update()
    {
        transform.position = player.position;
    }
}
