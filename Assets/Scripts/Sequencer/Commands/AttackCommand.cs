using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackCommand : Command
{
    NavMeshAgent agent;
    GameObject gameObject;
    GameObject targetObject;

    public static AttackCommand Init(GameObject gameObject) => 
        new AttackCommand() { gameObject = gameObject, agent = gameObject.GetComponent<NavMeshAgent>() };

    public override bool CheckCondition()
    {
        if (targetObject == null) return true;
        return Vector3.Distance(targetObject.gameObject.transform.position, gameObject.transform.position) <= 1.3f;
    }

    public override void Execute()
    {
        targetObject = Physics2D.OverlapCircle(gameObject.transform.position, 2.5f, LayerMask.GetMask("Enemy"))?.gameObject;
        if (targetObject == null) return;
        agent.SetDestination(targetObject.transform.position);
    }

    public override void ResetVariable()
    {
        
    }
}
