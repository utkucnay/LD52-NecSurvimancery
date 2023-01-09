using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ArrondPlayerCommand : Command
{
    NavMeshAgent agent;
    GameObject targetObject;
    Vector2 loc;

    public static ArrondPlayerCommand Init(NavMeshAgent agent, GameObject targetObject)
        => new ArrondPlayerCommand() { agent = agent, targetObject = targetObject };

    public override bool CheckCondition()
    {
        return true;
    }

    public override void Execute()
    {
        var dir = Vector2.up.Rotate(Random.Range(0, 360));
        loc = dir * targetObject.transform.position * Random.Range(.8f,3f);
        agent.SetDestination(loc);
    }

    public override void ResetVariable()
    {

    }
}
