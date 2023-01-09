using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowClosesetSkeletonCommand : Command
{
    GameObject gameObject;
    NavMeshAgent agent;
    public static FollowClosesetSkeletonCommand Init(GameObject gameObject) => 
        new FollowClosesetSkeletonCommand() { gameObject = gameObject, agent = gameObject.GetComponent<NavMeshAgent>()};

    public override bool CheckCondition()
    {
        return false;
    }

    public override void Execute()
    {
        var skeleton = AIManager.s_Instance.GetClosestSkeleton(gameObject.transform.position);
        if (skeleton == null) return;
        agent.SetDestination(skeleton.transform.position);
    }

    public override void ResetVariable()
    {
        
    }
}
