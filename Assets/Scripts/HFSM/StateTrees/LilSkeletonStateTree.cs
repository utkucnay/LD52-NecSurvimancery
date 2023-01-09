using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LilSkeletonStateTree : StateTree

{
    public enum States
    {
        kIdle,
        kCombat,
        kFollowPlayer,
        kMerge
    }

    public static LilSkeletonStateTree Init(GameObject gameObject) =>  new LilSkeletonStateTree(gameObject);

    public LilSkeletonStateTree(GameObject gameObject)
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
        var FollowPlayerSeqencer = Sequencer.Init();
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

        FollowPlayerSeqencer.AddCommand(ParallelCommand.Init(
            FollowTargetGameObjectCommand.Init( 
                gameObject.GetComponent<NavMeshAgent>() , GameObject.FindGameObjectWithTag("Player")), new Command[] { CharacterRotaterCommand.Init(gameObject) }));
        FollowPlayerSeqencer.AddCommand(TimerCommand.Init(.4f));

        CombatSeqencer.AddAction(() =>
        {
            SetData("IsAttack", false);
            SetData("IsBreakAttack", false);
        });
        CombatSeqencer.AddCommand(ParallelCommand.Init(AttackCommand.Init(gameObject,this),new Command[] { CharacterRotaterCommand.Init(gameObject) }));
        

        var IdleState = HierarchicalState.Init(IdleSeqencer, false);
        var CombatState = HierarchicalState.Init(CombatSeqencer);
        var FollowPlayerState = HierarchicalState.Init(FollowPlayerSeqencer);
        var MergePlayerState = HierarchicalState.Init(MergeSeqencer);

        IdleState.AddTransitions(Transition.Init(() => { return _currState.IsFinish; }, (int)States.kFollowPlayer));

        CombatState.AddTransitions(Transition.Init(() => { return (bool)dataMap["IsAttack"]; }, (int)States.kIdle));
        CombatState.AddTransitions(Transition.Init(() => { return _currState.IsFinish; }, (int)States.kFollowPlayer));

        FollowPlayerState.AddTransitions(Transition.Init(() => 
        { 
            return (bool)dataMap["EnemyFind"];
        }, (int)States.kCombat));

        CombatState.AddTransitions(Transition.Init(() => { return (bool)dataMap["IsMerge"]; }, (int)States.kFollowPlayer));
        IdleState.AddTransitions(Transition.Init(() => { return (bool)dataMap["IsMerge"]; }, (int)States.kFollowPlayer));
        FollowPlayerState.AddTransitions(Transition.Init(() => { return (bool)dataMap["IsMerge"]; }, (int)States.kFollowPlayer));

        _states[(int)States.kIdle] = IdleState;
        _states[(int)States.kCombat] = CombatState;
        _states[(int)States.kFollowPlayer] = FollowPlayerState;

        _currStateIndex = (int)States.kFollowPlayer;
        _currState = _states[(int)States.kFollowPlayer].Clone();
    }


}
