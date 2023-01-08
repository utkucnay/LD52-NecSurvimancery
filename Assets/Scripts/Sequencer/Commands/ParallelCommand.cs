using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallelCommand : Command
{
    Command mainCommand;
    Command[] parallelCommands;
    
    int _lenght;

    public static ParallelCommand Init(Command mainComand,Command[] parallelCommands)
    {
        return new ParallelCommand { mainCommand = mainComand , parallelCommands = parallelCommands, _lenght = parallelCommands.Length };
    }

    public override void Execute()
    {
        mainCommand.Execute();
        for (int i = 0; i < _lenght; i++)
        {
            parallelCommands[i].Execute();
        }
    }

    public override bool CheckCondition()
    {
        return mainCommand.CheckCondition();
    }

    public override void ResetVariable()
    {
        
    }
}
