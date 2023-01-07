using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionCommand : Command
{
    System.Action _action;

    public static ActionCommand Init(System.Action action) => new ActionCommand { _action = action };

    public override void Execute()
    {
        _action();
    }

    public override bool CheckCondition()
    {
        return true;
    }

    public override void ResetVariable()
    {
    }
}
