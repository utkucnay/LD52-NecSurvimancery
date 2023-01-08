using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : StateTree
{
    enum States
    {
        kMove,
        kDamage
    }

    States state = States.kMove;

    public static EnemyAI Init(GameObject gameObject) => new EnemyAI(gameObject);

    public EnemyAI(GameObject gameObject)
    {
        SetupTree(gameObject);
    }

    protected override void SetupTree(GameObject gameObject)
    {
        _states = new State[2];

        var MoveSeqancer = Sequencer.Init();

        MoveSeqancer.AddCommand(ParallelCommand.Init(
            TimerCommand.Init(
                .2f), new Command[] { FollowClosesetSkeletonCommand.Init(gameObject) }));
        MoveSeqancer.AddCommand(TimerCommand.Init(.4f));

        var MoveState = HierarchicalState.Init(MoveSeqancer);

        _states[(int)States.kMove] = MoveState;

        _currStateIndex = (int)state;
        _currState = _states[(int)state].Clone();

    }
}
