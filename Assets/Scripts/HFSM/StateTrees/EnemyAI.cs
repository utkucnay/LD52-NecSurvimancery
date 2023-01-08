using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : StateTree
{
    enum States
    {
        kMove,
        kTaunt
    }

    States state = States.kMove;

    public static EnemyAI Init(GameObject gameObject) => new EnemyAI(gameObject);

    public EnemyAI(GameObject gameObject)
    {
        SetupDataMap();
        SetupTree(gameObject);
    }

    protected override void SetupTree(GameObject gameObject)
    {
        _states = new State[2];

        var MoveSeqancer = Sequencer.Init();
        var TauntSeqancer = Sequencer.Init();

        MoveSeqancer.AddCommand(ParallelCommand.Init(
            TimerCommand.Init(
                .2f), new Command[] { FollowClosesetSkeletonCommand.Init(gameObject) }));
        MoveSeqancer.AddCommand(TimerCommand.Init(.4f));

        TauntSeqancer.AddAction(() => SetData("IsDamaged", false));
        TauntSeqancer.AddCommand(TimerCommand.Init(1.5f));
        

        var MoveState = HierarchicalState.Init(MoveSeqancer);
        var TauntState = HierarchicalState.Init(TauntSeqancer);


        MoveState.AddTransitions(Transition.Init(() => (bool)dataMap["IsDamaged"], (int)States.kTaunt));
        TauntState.AddTransitions(Transition.Init(() => _currState.IsFinish, (int)States.kMove));
        TauntState.AddTransitions(Transition.Init(() => (bool)dataMap["IsDamaged"], (int)States.kTaunt));

        _states[(int)States.kMove] = MoveState;
        _states[(int)States.kTaunt] = TauntState;

        _currStateIndex = (int)state;
        _currState = _states[(int)state].Clone();

    }

    protected override void SetupDataMap()
    {
        dataMap = new Dictionary<string, object>();
        dataMap.Add("IsDamaged", false);
    }
}
