using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Command : ICloneable
{
    public abstract void Execute();
    public abstract bool CheckCondition();
    public abstract void ResetVariable();

    public object Clone()
    {
        return this.MemberwiseClone();
    }
}
