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

    public void Damage(float damage, Vector2 dir)
    {
        healthSubsystem.Damage(damage);
        PushSelf(damage, dir);
        stateTree.SetData("IsDamaged", true);
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

    public void PushSelf(float amount, Vector2 dir)
    {
        agent.ResetPath();
        agent.velocity = amount * dir;
    }
}
