using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HierarchicalState : State
{
    public Sequencer _commandQueue;

    public bool _isLoop;

    public override bool IsFinish => _commandQueue.IsFinish;

    public override bool IsLoop => _isLoop;

    public static HierarchicalState Init(Sequencer commandQueue, bool IsLoop = true)
    {
        var state = new HierarchicalState { _commandQueue = commandQueue, _isLoop = IsLoop, _transitions = new List<Transition>() };
        return state;
    }

    public override void Execute()
    {
        _commandQueue.Execute();
    }

    public override int CheckTransitions()
    {
        for (int i = 0; i < _transitions.Count; i++)
        {
            var transition = _transitions[i];
            if (transition._condition.CheckCondition()) return transition._stateIndex;
        }
        return -1;
    }

    public override State Clone()
    {
        return new HierarchicalState { _commandQueue = this._commandQueue.Clone(), _transitions = new List<Transition>(this._transitions), _isLoop = _isLoop };
    }
}
