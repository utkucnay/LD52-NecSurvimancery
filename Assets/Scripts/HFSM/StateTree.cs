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

    public Dictionary<string, object> dataMap;

    [SerializeField] bool _debug;
    protected abstract void SetupTree(GameObject gameObject);

    protected abstract void SetupDataMap();
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

    public int GetCurrState()
    {
        return _currStateIndex;
    }

    public void SetData<T>(string key, T value)
    {
        object originData;
        if(dataMap.TryGetValue(key, out originData))
        {
            dataMap[key] = value;
        }
        
    }
}
