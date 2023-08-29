using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterAI : MonoBehaviour
{
    [SerializeField] Transform objective;
    NavMeshAgent agent;
    Animator animator;
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        StartCoroutine(UpdateDestination());
    }
    IEnumerator UpdateDestination()
    {
        while (true)
        {
            agent.destination = objective.position;
            yield return new WaitForSeconds(1);
        }
    }
    private void Update()
    {
        animator.SetBool("IsWalking", agent.velocity != Vector3.zero);
    }
}
