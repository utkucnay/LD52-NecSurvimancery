using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamagable 
{
    NavMeshAgent agent;
    StateTree stateTree;
    [SerializeField] public float attackPower;

    [SerializeField] HealthSubsystem healthSubsystem;

    public void Damage(float damage)
    {
        healthSubsystem.Damage(damage);
    }

    private void Start()
    {
        stateTree = EnemyAI.Init(gameObject);
        healthSubsystem.Start(gameObject);
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    private void Update()
    {
        stateTree.Execution();
    }
}
