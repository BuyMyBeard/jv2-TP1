using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
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
            yield return null;
        }
    }
    private void Update()
    {
        animator.SetBool("IsWalking", agent.velocity != Vector3.zero);
        animator.SetBool("IsJumping", agent.isOnOffMeshLink);
    }

    private void OnTriggerEnter(Collider other)
    {
        FindObjectOfType<GameStateManager>().Lose();
    }
}
