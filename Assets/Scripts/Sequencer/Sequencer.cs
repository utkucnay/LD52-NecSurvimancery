using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Sequencer
{
    Queue<Command> _commandQueue;

    public static Sequencer Init() => new Sequencer { _commandQueue = new Queue<Command>() };
    public bool IsFinish { get => _commandQueue.Count <= 0; }

    public void Execute()
    {
        var command = _commandQueue.Peek();
        if (command == null) { RemoveQueue(); return; }
        command.Execute();
        if (command.CheckCondition()) { command.ResetVariable(); RemoveQueue(); }
    }

    public void ClearQueue()
    {
        _commandQueue.Clear();
    }

    public void AddCommand(Command command)
    {
        _commandQueue.Enqueue(command);
    }

    public void AddAction(System.Action action)
    {
        _commandQueue.Enqueue(ActionCommand.Init(action));
    }

    public int Count()
    {
        return _commandQueue.Count;
    }

    void RemoveQueue()
    {
        _commandQueue.Dequeue();
    }

    public Sequencer Clone()
    {
        return new Sequencer { _commandQueue = this._commandQueue.Clone<Command>() };
    }
}
