using System;

/// <summary>
/// ネットワークでやりとりするデータ形式
/// </summary>
[Serializable]
public struct Data
{
    /// <summary>実行した命令</summary>
    public Command Command;
    /// <summary>命令の対象となるプレイヤー</summary>
    public int TargetPlayer;
    /// <summary>命令の subject となる値</summary>
    public int Value;

    /// <summary>
    /// コンストラクター
    /// </summary>
    /// <param name="command"></param>
    /// <param name="targetPlayer"></param>
    /// <param name="value"></param>
    public Data(Command command, int targetPlayer, int value)
    {
        Command = command;
        TargetPlayer = targetPlayer;
        Value = value;
    }
}

/// <summary>
/// 自分の番で実行する命令
/// </summary>
[Serializable]
public enum Command
{
    /// <summary>株価を上げる</summary>
    Raise,
    /// <summary>株を買う</summary>
    Buy,
    /// <summary>株を売る</summary>
    Sell,
}
