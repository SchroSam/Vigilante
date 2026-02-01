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
    bool hasForcedAct = false;

    public int maxHealth = 100;
    int health;

    public bool blocking;
    public Vector2Int dir;
    public List<Collider> objectsHit
    {
        get {
            print("objectsHit being accessed, it has " + _objectsHit.Count + " items");
            for (int i = 0; i < _objectsHit.Count; i++)
            {
                print(i + " : " + _objectsHit[i]);
            }
            return _objectsHit;
        }
        set
        {
            _objectsHit = value;
        }
    }
    List<Collider> _objectsHit = new List<Collider>();

    public void ClearObjectsHit()
    {
        print("clearing objects hit");
        _objectsHit.Clear();
    }

    public void AddToObjectsHit(Collider c)
    {
        print("adding " + c);
        _objectsHit.Add(c);
    }

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
        curAct = defaultAct;
        defaultAct.Enter(new InputPackage());
        health = maxHealth;
    }

    public void Reset()
    {
        curAct = defaultAct;
        defaultAct.Enter(new InputPackage());
        health = maxHealth;
        animator.Play("Idle");
    }

    public void HandleHit(HitInfo hit)
    {
        print(hit.dir);
        print(new Vector2Int(-dir.x, dir.y));
        if (blocking && hit.dir == new Vector2Int(-dir.x, dir.y))
        {
            print("blocked");
            hit.source.SetForcedAct("Hurt");
        } else
        {
            print("hit");
            TakeDamage(hit.dmg);
        }
    }

    public void TakeDamage(int dmg)
    {
        if (curAct == acts["Die"]) {
            return;
        }
        health -= dmg;
        if (health <= 0)
        {
            health = 0;
            forcedAct = "Die";
            //Lose screen function here
            if(gameObject.tag == "Player")
                FindFirstObjectByType<LoseUI>().LoseScreen();
        } else
        {
            SetForcedAct("Hurt");
        }
    }

    public void SetForcedAct(string act)
    {
        forcedAct = act;
        hasForcedAct = true;
    }

    public void Process(InputPackage input)
    {
        string nextAct = "";
        if (hasForcedAct)
        {
            hasForcedAct = false;
            print("forced act: " + forcedAct);
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
        print("switching action from " + curAct);
        curAct.Exit();
        curAct = nextAct;
        print("switching to " + curAct.gameObject.name);
        curAct.Enter(input);
    }
}
