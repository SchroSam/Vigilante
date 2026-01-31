using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Nav : MonoBehaviour
{
    NavMeshAgent m_Agent;
    CharacterBehavior behavior;
    public GameObject target;
    InputPackage nextInput;
    Vector3 movDir3;

    void Start()
    {
        behavior = GetComponent<CharacterBehavior>();
        m_Agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log($"x:{m_Agent.steeringTarget.x} z:{m_Agent.steeringTarget.z}");
        movDir3 = (target.transform.position - transform.position).normalized;

        nextInput.movedir.x = movDir3.x;
        nextInput.movedir.y = movDir3.y;

        behavior.Process(nextInput);

        m_Agent.destination = target.transform.position;
    }
}
