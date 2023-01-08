using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowTargetGameObjectCommand : Command
{
    NavMeshAgent agent;
    GameObject targetObject;

    public static FollowTargetGameObjectCommand Init(NavMeshAgent agent, GameObject targetObject) 
        => new FollowTargetGameObjectCommand() { agent= agent, targetObject = targetObject };

    public override bool CheckCondition()
    {
        return true;
    }

    public override void Execute()
    {
        agent.SetDestination(targetObject.transform.position);
    }

    public override void ResetVariable()
    {
        
    }
}
