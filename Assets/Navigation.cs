using UnityEngine;
using UnityEngine.AI;

[RequireComponent (typeof(NavMeshAgent))]
public class Navigation : MonoBehaviour
{
    private NavMeshAgent m_Agent;
    public float wanderRadius = 10f;
    public float wanderInterval = 5f;
    private float timer;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_Agent = GetComponent<NavMeshAgent>();
        timer = wanderInterval;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= wanderInterval)
        {
            Vector3 newDestintation;
            if (RandomPointOnNavMesh(transform.position, wanderRadius, out newDestintation))
            {
                m_Agent.SetDestination(newDestintation);
            }

            timer = 0f;
        }
    }

  private bool RandomPointOnNavMesh(Vector3 origin, float maxDistance, out Vector3 result)
    {
        Vector3 randomDir = Random.insideUnitSphere * maxDistance;
        randomDir += origin;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDir, out hit, maxDistance, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;

    }
}
