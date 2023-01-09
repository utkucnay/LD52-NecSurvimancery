using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackCommand : Command
{
    NavMeshAgent agent;
    GameObject gameObject;
    GameObject targetObject;
    StateTree stateTree;

    public static AttackCommand Init(GameObject gameObject, StateTree stateTree) => 
        new AttackCommand() { gameObject = gameObject, agent = gameObject.GetComponent<NavMeshAgent>(), stateTree = stateTree };

    public override bool CheckCondition()
    {
        if (targetObject == null) return true;
        if (targetObject.activeSelf == false) return true;
        return Vector2.Distance(targetObject.transform.position,gameObject.transform.position) <= .501f;
    }

    public override void Execute()
    {
        targetObject = Physics2D.OverlapCircle(gameObject.transform.position, (float)stateTree.dataMap["WarningRange"], LayerMask.GetMask("Enemy"))?.gameObject;
        if (targetObject == null) return;
        agent.velocity = (targetObject.transform.position - gameObject.transform.position).normalized * (float)stateTree.dataMap["DashSpeed"];
    }

    public override void ResetVariable()
    {
        
    }
}
