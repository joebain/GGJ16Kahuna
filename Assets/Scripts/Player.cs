using UnityEngine;

public class Player : MonoBehaviour
{
    public Inventory Inventory = new Inventory();

    private NavMeshAgent agent;

    private GameObject targetIndicator;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        targetIndicator = transform.Find("Target").gameObject;
        targetIndicator.SetActive(false);
    }

    public void GoTo(Vector3 position)
    {
        agent.destination = position;
        targetIndicator.transform.position = position;
        targetIndicator.SetActive(true);
    }

    void Update()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            targetIndicator.SetActive(false);
        } else
        {
            targetIndicator.transform.position = agent.destination;
        }
    }
}
