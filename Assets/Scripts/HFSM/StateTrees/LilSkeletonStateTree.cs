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
        kFollowPlayer
    }

    public static LilSkeletonStateTree Init(GameObject gameObject) =>  new LilSkeletonStateTree(gameObject);

    public LilSkeletonStateTree(GameObject gameObject)
    {
        SetupTree(gameObject);
    }

    protected override void SetupTree(GameObject gameObject)
    {
        _states = new State[3];

        var IdleSeqancer = Sequencer.Init();
        var CombatSeqancer = Sequencer.Init();
        var FollowPlayerSeqancer = Sequencer.Init();

        IdleSeqancer.AddCommand(MaterialChangeColorCommand.Init(gameObject.GetComponent<SpriteRenderer>()?.material, Color.red, .2f));
        IdleSeqancer.AddCommand(TimerCommand.Init(.7f));
        IdleSeqancer.AddCommand(MaterialChangeColorCommand.Init(gameObject.GetComponent<SpriteRenderer>()?.material, Color.white, .2f));
        IdleSeqancer.AddCommand(TimerCommand.Init(.2f));


        FollowPlayerSeqancer.AddCommand(ParallelCommand.Init(
            FollowTargetGameObjectCommand.Init( 
                gameObject.GetComponent<NavMeshAgent>() , GameObject.FindGameObjectWithTag("Player")), new Command[] { CharacterRotaterCommand.Init(gameObject) }));
        FollowPlayerSeqancer.AddCommand(TimerCommand.Init(.4f));

        CombatSeqancer.AddCommand(AttackCommand.Init(gameObject));

        var IdleState = HierarchicalState.Init(IdleSeqancer, false);
        var CombatState = HierarchicalState.Init(CombatSeqancer, false);
        var FollowPlayerState = HierarchicalState.Init(FollowPlayerSeqancer);

        IdleState.AddTransitions(Transition.Init(() => { return _currState.IsFinish; }, (int)States.kFollowPlayer));
        CombatState.AddTransitions(Transition.Init(() => { return _currState.IsFinish; }, (int)States.kIdle));

        FollowPlayerState.AddTransitions(Transition.Init(() => 
        { 
            return Physics2D.OverlapCircle(gameObject.transform.position, 2.5f, LayerMask.GetMask("Enemy")) != null;
        }, (int)States.kCombat));

        _states[(int)States.kIdle] = IdleState;
        _states[(int)States.kCombat] = CombatState;
        _states[(int)States.kFollowPlayer] = FollowPlayerState;

        _currStateIndex = (int)States.kFollowPlayer;
        _currState = _states[(int)States.kFollowPlayer].Clone();
    }
}
