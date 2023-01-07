using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuncCondition : Condition
{
    public System.Func<bool> _condition;

    public static FuncCondition Init(System.Func<bool> condition) => new FuncCondition { _condition = condition };

    public override bool CheckCondition() => _condition();
}
