using UnityEngine;
using System.Collections.Generic;

public class CharacterBehavior : MonoBehaviour
{
    CharAction curAct;
    public CharAction defaultAct;
    public GameObject actContainer;
    Dictionary<string, CharAction> acts = new Dictionary<string, CharAction>();

    public CharacterController controller;
    public Animator animator;

    public string forcedAct;

    public int maxHealth = 100;
    int health;

    public bool blocking;
    public bool attacking;
    public Vector2Int dir;

    private void Start()
    {
        CharAction[] children = actContainer.GetComponentsInChildren<CharAction>();
        foreach (CharAction c in children)
        {
            acts.Add(c.gameObject.name, c);
            c.behavior = this;
            c.controller = controller;
            c.animator = animator;
        }
        SwitchAction(defaultAct, new InputPackage());
        health = maxHealth;
    }

    public void Reset()
    {
        SwitchAction(defaultAct, new InputPackage());
        health = maxHealth;
        animator.Play("Idle");
    }

    public void HandleHit(HitInfo hit)
    {
        if (blocking && hit.dir == new Vector2Int(-hit.dir.x, hit.dir.y))
        {
            print("blocked");
        } else
        {
            print("hit");
            TakeDamage(hit.dmg);
        }
    }

    public void TakeDamage(int dmg)
    {
        health -= dmg;
        if (health <= 0)
        {
            health = 0;
            forcedAct = "Die";
        } else
        {
            forcedAct = "Hurt";
        }
    }

    public void Process(InputPackage input)
    {
        string nextAct = "";
        if (forcedAct != null)
        {
            nextAct = forcedAct;
            forcedAct = null;
        } else
        {
            nextAct = curAct.GetNextAction(input);
        }
        if (!string.IsNullOrWhiteSpace(nextAct))
        {
            CharAction next;
            bool found = acts.TryGetValue(nextAct, out next);
            if (!found) { print("Unknown character action " + nextAct + " requested"); }
            else
            {
                SwitchAction(next, input);
            }
        }
        curAct.Process(input);
    }

    void SwitchAction(CharAction nextAct, InputPackage input)
    {
        if (curAct) curAct.Exit();
        curAct = nextAct;
        print("switching to " + curAct.gameObject.name);
        curAct.Enter(input);
    }
}
