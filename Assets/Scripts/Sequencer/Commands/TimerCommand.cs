using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerCommand : Command
{
    public float _time;
    float _currtime;

    public static TimerCommand Init(float time) => new TimerCommand { _time = time };

    public override void Execute()
    {
        _currtime += Time.deltaTime;
    }

    public override bool CheckCondition()
    {
        if (_currtime >= _time) return true;
        return false;
    }

    public override void ResetVariable()
    {
        _currtime = 0;
    }
}
