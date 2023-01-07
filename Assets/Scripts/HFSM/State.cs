using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    public List<Transition> _transitions;

    public void AddTransitions(Transition transition)
    {
        _transitions.Add(transition);
    }

    public abstract bool IsFinish { get; }
    public abstract bool IsLoop { get; }
    public abstract void Execute();
    public abstract int CheckTransitions();
    public abstract State Clone();
}
