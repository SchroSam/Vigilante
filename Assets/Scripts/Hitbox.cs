using UnityEngine;

public class Hitbox : MonoBehaviour
{
    public CharacterBehavior behavior;

    private void OnTriggerEnter(Collider other)
    {
        if (!behavior.objectsHit.Contains(other))
        {
            behavior.objectsHit.Add(other);
            CharacterBehavior oBeh = other.GetComponent<CharacterBehavior>();
            HitInfo h = new HitInfo();
            h.dir = behavior.dir;
            h.dmg = 15;
            oBeh.HandleHit(h);
        }
    }
}
