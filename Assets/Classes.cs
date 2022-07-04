using System;

[Serializable]
public struct Data
{
    public Command Command;
    public int TargetPlayer;
    public int Value;

    public Data(Command command, int targetPlayer, int value)
    {
        Command = command;
        TargetPlayer = targetPlayer;
        Value = value;
    }
}

[Serializable]
public enum Command
{
    /// <summary>Š”‰¿‚ğã‚°‚é</summary>
    Raise,
}
