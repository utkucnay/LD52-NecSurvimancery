using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LilSkeleton : MonoBehaviour, IDamagable
{
    NavMeshAgent agent;
    StateTree stateTree;

    [SerializeField] float AttackPower = 0;

    [SerializeField] HealthSubsystem healthSubsystem;

    public void Damage(float damage)
    {
        healthSubsystem.Damage(damage);
    }

    private void Start()
    {
        stateTree = LilSkeletonStateTree.Init(gameObject);
        healthSubsystem.Start(gameObject);
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    private void Update()
    {
        stateTree.Execution();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            switch((LilSkeletonStateTree.States)stateTree.GetCurrState())
            {
                case LilSkeletonStateTree.States.kCombat:
                    collision.gameObject.GetComponent<Enemy>().Damage(AttackPower);
                    break;
                case LilSkeletonStateTree.States.kIdle:
                   healthSubsystem.Damage(collision.gameObject.GetComponent<Enemy>().attackPower);
                    break;
            }
        }
    }
}
