using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LilSkeletonStateTree : StateTree

{
    enum States
    {
        kIdle,
        kCombat,
        kFollowPlayer
    }

    States state = States.kFollowPlayer;

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



        var IdleState = HierarchicalState.Init(IdleSeqancer, false);
        var CombatState = HierarchicalState.Init(CombatSeqancer, false);
        var FollowPlayerState = HierarchicalState.Init(FollowPlayerSeqancer);

        _states[(int)States.kIdle] = IdleState;
        _states[(int)States.kCombat] = CombatState;
        _states[(int)States.kFollowPlayer] = FollowPlayerState;

        _currStateIndex = (int)state;
        _currState = _states[(int)state].Clone();
    }
}
