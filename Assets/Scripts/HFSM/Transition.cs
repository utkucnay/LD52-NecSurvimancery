using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition
{
    public static Transition Init(Condition condition, int stateIndex) =>
        new Transition { _condition = condition, _stateIndex = stateIndex };

    public static Transition Init(System.Func<bool> func, int stateIndex) =>
        new Transition { _condition = FuncCondition.Init(func), _stateIndex = stateIndex };

    public Condition _condition;
    public int _stateIndex;
}
