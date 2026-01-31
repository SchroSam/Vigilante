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
    public GameObject pusher;
    bool pushed = false;

    public float pushDist = 0.3f;
    public float pushReduce = 2f;

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
        if(m_Agent.steeringTarget != null && !pushed)
            movDir3 = (m_Agent.steeringTarget - new Vector3(transform.position.x, transform.position.y, transform.position.z)).normalized;
        else if (pushed)
            movDir3 = (transform.position - pusher.transform.position).normalized + m_Agent.steeringTarget.normalized * pushReduce;

        nextInput.movedir.x = movDir3.x;
        nextInput.movedir.y = movDir3.z;

        nextInput.action = "Move";

        behavior.Process(nextInput);

        m_Agent.destination = target.transform.position;

        if(pushed && Vector3.Distance(transform.position, pusher.transform.position) > pushDist)
        {
            pushed = false;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if(other.tag == "Wall")
        {
            pusher = other.gameObject;
            pushed = true;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            pusher = collision.gameObject;
            pushed = true;
        }
    }
}
