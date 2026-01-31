using UnityEngine;
using System.Collections.Generic;

public class CharacterBehavior : MonoBehaviour
{
    CharAction curAct;
    public CharAction defaultAct;
    public GameObject actContainer;
    Dictionary<string, CharAction> acts = new Dictionary<string, CharAction>();

    public CharacterController controller;

    public string forcedAct;

    private void Start()
    {
        CharAction[] children = actContainer.GetComponentsInChildren<CharAction>();
        foreach (CharAction c in children)
        {
            acts.Add(c.gameObject.name, c);
            c.controller = controller;
        }
        SwitchAction(defaultAct);
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
                SwitchAction(next);
            }
        }
        curAct.Process(input);
    }

    void SwitchAction(CharAction nextAct)
    {
        if (curAct) curAct.Exit();
        curAct = nextAct;
        curAct.Enter();
    }
}
