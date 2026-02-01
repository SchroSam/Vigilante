using UnityEngine;

public class RegenerateHealth : MonoBehaviour
{
    public CharacterBehavior behav;
    public int amount = 1;
    public float rate = 1f;
    float next;

    private void Start()
    {
        next = Time.time + rate;
    }
    void FixedUpdate()
    {
        if (Time.time > next)
        {
            next += rate;
            behav.Heal(amount);
        }
    }
}
