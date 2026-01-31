using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Nav : MonoBehaviour
{
    NavMeshAgent m_Agent;
    public GameObject target;

    void Start()
    {
        m_Agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log($"x:{m_Agent.steeringTarget.x} z:{m_Agent.steeringTarget.z}");
        m_Agent.destination = target.transform.position;
    }
}
