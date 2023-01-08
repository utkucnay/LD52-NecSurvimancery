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
        if (targetObject.activeSelf == false) return true;
        return Vector2.Distance(targetObject.transform.position,gameObject.transform.position) <= .501f;
    }

    public override void Execute()
    {
        targetObject = Physics2D.OverlapCircle(gameObject.transform.position, 3f, LayerMask.GetMask("Enemy"))?.gameObject;
        if (targetObject == null) return;
        agent.velocity = (targetObject.transform.position - gameObject.transform.position).normalized * 10f;
    }

    public override void ResetVariable()
    {
        
    }
}
