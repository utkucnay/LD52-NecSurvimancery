using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateTree
{
    protected State[] _states;
    protected State _currState;
    protected int _currStateIndex;

    [SerializeField] bool trans;
    [SerializeField] bool loop;

    [SerializeField] bool _debug;
    protected abstract void SetupTree(GameObject gameObject);

    public void Execution()
    {
        if (_currState.IsFinish)
        {
            if (_currState.IsLoop) { _currState = _states[_currStateIndex].Clone(); return; }
        }
        else
            _currState.Execute();

        var stateIndex = _currState.CheckTransitions();
        if (stateIndex != -1) 
        { 
            _currState = _states[stateIndex].Clone();
            _currStateIndex = stateIndex; 
        }
    }
}
