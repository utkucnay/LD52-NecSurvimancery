using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LilSkeleton : MonoBehaviour
{
    NavMeshAgent agent;
    LilSkeletonStateTree stateTree;

    private void Start()
    {
        stateTree = LilSkeletonStateTree.Init(gameObject);
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    private void Update()
    {
        stateTree.Execution();
    }
}
