using UnityEngine;

public class NPC : MonoBehaviour
{
    private NavMeshAgent navAgent;

	public enum State
	{
		Walking,
		Homing,
		Done
	}
	public State state = State.Walking;

    private Vector3 startPoint;

	GameObject player;
	float startSpeed;

    public void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();

        startPoint = transform.position;

		player = GameObject.Find("Player");

		startSpeed = navAgent.speed;
    }

    public void Update()
    {
        switch (state)
        {
		case State.Walking:
			{
            if (navAgent.isOnNavMesh && navAgent.remainingDistance <= navAgent.stoppingDistance)
            {
                navAgent.destination = startPoint + Random.insideUnitSphere * 20f;
            }
			} break;
		case State.Homing:
		{
			navAgent.speed = startSpeed * 3;
			navAgent.destination = player.transform.position;
			if (navAgent.isOnNavMesh && navAgent.remainingDistance <= 5)
			{
				Player p = player.GetComponent<Player>();
				p.log.actions.Add("villagers_arrived");
					state = State.Done;
			}
			}break;
		case State.Done:
			{
			}break;
		}
	}

	public void WalkToPlayer()
	{
		state = State.Homing;
	}

	public void GoToTemple()
	{
		state = State.Done;
		navAgent.destination = new Vector3(-39,2,-26);
	}
}
