using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MidSkeleton : MonoBehaviour
{
    NavMeshAgent agent;
    StateTree stateTree;

    [SerializeField] float AttackPower = 0;
    [SerializeField] float range = 2.5f;
    [SerializeField] float dashSpeed = 10f;

    [SerializeField] HealthSubsystem healthSubsystem;
    SpriteRenderer sr;


    float timer = .51f;
    bool isTimerRun;
    const float limitTimer = .5f;

    public void Damage(float damage, Vector2 dir)
    {
        PushSelf(damage, dir);
        var seq = DOTween.Sequence();
        seq.Append(sr.material.DOColor(Color.red, .2f));
        seq.Append(sr.material.DOColor(Color.white, .2f));
        healthSubsystem.Damage(damage);
    }

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        stateTree = MidSkeletonStateTree.Init(gameObject);
        healthSubsystem.Start(gameObject, OnDie);
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        stateTree.SetData("WarningRange", range);
        stateTree.SetData("DashSpeed", dashSpeed);
    }

    private void Update()
    {
        stateTree.SetData("EnemyFind", Physics2D.OverlapCircle(transform.position, range, LayerMask.GetMask("Enemy")) != null);

        stateTree.Execution();

        Debug.Log(((LilSkeletonStateTree.States)stateTree.GetCurrState()).ToString());

        timer += Time.deltaTime;

        var target = Physics2D.OverlapCircle(transform.position, .7f, LayerMask.GetMask("Enemy"));
        if (target != null)
        {
            if (timer > limitTimer)
            {
                Vector2 dir;
                switch ((LilSkeletonStateTree.States)stateTree.GetCurrState())
                {
                    case LilSkeletonStateTree.States.kCombat:
                        dir = target.transform.position - transform.position;
                        dir = dir.normalized;
                        target.gameObject.GetComponent<Enemy>().Damage(AttackPower, dir);
                        stateTree.SetData("IsAttack", true);
                        PushSelf(.2f, -dir);
                        break;
                    case LilSkeletonStateTree.States.kIdle:
                        dir = transform.position - target.transform.position;
                        dir = dir.normalized;
                        Damage(target.gameObject.GetComponent<Enemy>().attackPower, dir);
                        target.gameObject.GetComponent<Enemy>().PushSelf(.2f, -dir);
                        break;

                }
                timer = 0;
            }
        }



    }

    void OnDie()
    {
        AIManager.s_Instance.RemoveMidSkeleton(this.gameObject);
    }

    public void PushSelf(float amount, Vector2 dir)
    {
        if (gameObject.activeSelf == false) return;
        agent.ResetPath();
        agent.velocity = amount * dir;
    }
}
