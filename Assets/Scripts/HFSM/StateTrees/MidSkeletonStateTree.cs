using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MidSkeletonStateTree : StateTree
{
    public enum States
    {
        kIdle,
        kCombat,
        kArroundPlayer,
        kMerge
    }

    public static MidSkeletonStateTree Init(GameObject gameObject) => new MidSkeletonStateTree(gameObject);

    public MidSkeletonStateTree(GameObject gameObject)
    {
        SetupDataMap();
        SetupTree(gameObject);
    }

    protected override void SetupDataMap()
    {
        dataMap = new Dictionary<string, object>();
        dataMap.Add("IsAttack", false);
        dataMap.Add("IsBreakAttack", false);
        dataMap.Add("EnemyFind", false);
        dataMap.Add("WarningRange", .5f);
        dataMap.Add("DashSpeed", 0);
        dataMap.Add("IsMerge", false);
    }

    protected override void SetupTree(GameObject gameObject)
    {
        _states = new State[3];

        var IdleSeqencer = Sequencer.Init();
        var CombatSeqencer = Sequencer.Init();
        var ArroundPlayerSeqencer = Sequencer.Init();
        var MergeSeqencer = Sequencer.Init();

        IdleSeqencer.AddAction(() =>
        {
            var agent = gameObject.GetComponent<NavMeshAgent>();
            agent.velocity = (-agent.velocity).normalized * 2f;
        });

        IdleSeqencer.AddCommand(TimerCommand.Init(1.5f));

        MergeSeqencer.AddAction(() =>
        {
            var agent = gameObject.GetComponent<NavMeshAgent>();
            agent.velocity = Vector3.zero;
            agent.ResetPath();
        });
        MergeSeqencer.AddCommand(TimerCommand.Init(100000));

        ArroundPlayerSeqencer.AddCommand(ParallelCommand.Init(
            ArrondPlayerCommand.Init(
                gameObject.GetComponent<NavMeshAgent>(), GameObject.FindGameObjectWithTag("Player")), new Command[] { CharacterRotaterCommand.Init(gameObject) }));
        ArroundPlayerSeqencer.AddCommand(TimerCommand.Init(1.2f));

        CombatSeqencer.AddAction(() =>
        {
            SetData("IsAttack", false);
            SetData("IsBreakAttack", false);
        });
        CombatSeqencer.AddCommand(ParallelCommand.Init(AttackCommand.Init(gameObject, this), new Command[] { CharacterRotaterCommand.Init(gameObject) }));


        var IdleState = HierarchicalState.Init(IdleSeqencer, false);
        var CombatState = HierarchicalState.Init(CombatSeqencer);
        var ArroundPlayerState = HierarchicalState.Init(ArroundPlayerSeqencer);
        var MergePlayerState = HierarchicalState.Init(MergeSeqencer);

        IdleState.AddTransitions(Transition.Init(() => { return _currState.IsFinish; }, (int)States.kArroundPlayer));

        CombatState.AddTransitions(Transition.Init(() => { return (bool)dataMap["IsAttack"]; }, (int)States.kIdle));
        CombatState.AddTransitions(Transition.Init(() => { return _currState.IsFinish; }, (int)States.kArroundPlayer));

        ArroundPlayerState.AddTransitions(Transition.Init(() =>
        {
            return (bool)dataMap["EnemyFind"];
        }, (int)States.kCombat));

        CombatState.AddTransitions(Transition.Init(() => { return (bool)dataMap["IsMerge"]; }, (int)States.kArroundPlayer));
        IdleState.AddTransitions(Transition.Init(() => { return (bool)dataMap["IsMerge"]; }, (int)States.kArroundPlayer));
        ArroundPlayerState.AddTransitions(Transition.Init(() => { return (bool)dataMap["IsMerge"]; }, (int)States.kArroundPlayer));

        _states[(int)States.kIdle] = IdleState;
        _states[(int)States.kCombat] = CombatState;
        _states[(int)States.kArroundPlayer] = ArroundPlayerState;

        _currStateIndex = (int)States.kArroundPlayer;
        _currState = _states[(int)States.kArroundPlayer].Clone();
    }
}
