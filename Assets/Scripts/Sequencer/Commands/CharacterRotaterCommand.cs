using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterRotaterCommand : Command
{
    GameObject agentGO;
    NavMeshAgent agent;

    static readonly Vector3 RightSide = new Vector3(-1, 1, 1);
    static readonly Vector3 LeftSide = new Vector3(1, 1, 1);

    public static CharacterRotaterCommand Init(GameObject agentGO) => new CharacterRotaterCommand() { agentGO = agentGO, agent = agentGO.GetComponent<NavMeshAgent>() };

    public override bool CheckCondition()
    {
        return true;
    }

    public override void Execute()
    {
        if (agent.velocity.x > 0)
            agentGO.transform.localScale = RightSide;
        else if(agent.velocity.x < 0)
            agentGO.transform.localScale = LeftSide;
    }

    public override void ResetVariable()
    {
        
    }
}
