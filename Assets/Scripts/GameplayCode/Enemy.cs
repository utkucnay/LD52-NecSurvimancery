using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamagable 
{
    public ParticleSystem blood;

    NavMeshAgent agent;
    StateTree stateTree;
    [SerializeField] public float attackPower;

    [SerializeField] HealthSubsystem healthSubsystem;

    public ObjectType objectType;

    public void Damage(float damage, Vector2 dir)
    {
        PushSelf(damage, dir);
        stateTree.SetData("IsDamaged", true);
        healthSubsystem.Damage(damage);
    }

    private void Start()
    {
        stateTree = EnemyAI.Init(gameObject);
        healthSubsystem.Start(gameObject, OnDie);
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    private void Update()
    {
        stateTree.Execution();
    }

    void OnDie()
    {
        AIManager.s_Instance.AddLilSkeleton(this.gameObject);
        ObjectPool.s_Instance.SetObject(objectType, this.gameObject);
        GameManager.s_Instance.souls += 1;
    }

    public void PushSelf(float amount, Vector2 dir)
    {
        agent.ResetPath();
        agent.velocity = amount * dir;
    }

    void CreateBlood()
    {
        blood.Play();
        Debug.Log("kan");
    }
}
