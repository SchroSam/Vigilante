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
        m_Agent.isStopped = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log($"x:{m_Agent.steeringTarget.x} z:{m_Agent.steeringTarget.z}");
        if(m_Agent.steeringTarget != null)
            movDir3 = (m_Agent.steeringTarget - new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z)).normalized;

        nextInput.movedir.x = movDir3.x;
        nextInput.movedir.y = movDir3.z;

        nextInput.action = "Move";

        behavior.Process(nextInput);

        m_Agent.destination = target.transform.position;
    }
}
