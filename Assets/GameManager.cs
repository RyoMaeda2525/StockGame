using System;
using UnityEngine;
// Photon 用の名前空間を参照する
using Photon.Pun;
using Photon.Pun.UtilityScripts;    // PunTurnManager, IPunTurnManagerCallbacks を使うため
using Photon.Realtime;

/// <summary>
/// ゲーム・ターンを管理するコンポーネント
/// </summary>
public class GameManager : MonoBehaviour, IPunTurnManagerCallbacks
{
    /// <summary>操作をするためのパネル (UI)</summary>
    [SerializeField] GameObject _controlPanel;
    /// <summary>プレイヤーの出現ポイント</summary>
    [SerializeField] Transform[] _spawnPositions;
    /// <summary>プレイヤーのプレハブ名</summary>
    [SerializeField] string _playerPrefabName = "Player";
    /// <summary>プレイヤーの Rigidbody</summary>
    [SerializeField] Rigidbody _player;
    /// <summary>弾くパワーの倍率</summary>
    [SerializeField] float _powerScale = 1f;
    [SerializeField] PunTurnManager _turnManager;
    [SerializeField] UIManager _uiManager;
    /// <summary>自分が何番目のプレイヤーか（0スタート。途中抜けを考慮していない）</summary>
    int _playerIndex = -1;
    /// <summary>現在何番目のプレイヤーが操作をしているか（0スタート。途中抜けを考慮していない）</summary>
    int _activePlayerIndex = -1;
    /// <summary>現在の自分の株価</summary>
    int _stockPrice = 1;

    /// <summary>
    /// ゲームを初期化する
    /// </summary>
    void InitializeGame()
    {
        Debug.Log("Initialize Game...");
        _playerIndex = Array.IndexOf(PhotonNetwork.PlayerList, PhotonNetwork.LocalPlayer);
    }

    bool IsMyTurn()
    {
        return _activePlayerIndex == _playerIndex;
    }

    void PrepareControl()
    {
        _controlPanel.SetActive(IsMyTurn());
    }

    void ParseData(Data data)
    {
        if (data.Command == Command.Raise)
        {
            RaiseStock(data.TargetPlayer, data.Value);
        }
    }

    void RaiseStock(int playerIndex, int targetPrice)
    {
        print($"Raise player {playerIndex}'s stock to {targetPrice}");
        _uiManager.RaiseStock(playerIndex, targetPrice);
    }

    /// <summary>
    /// 株価を 1 上げる
    /// ボタンから呼ばれ、PunTurnManager に Move を送る
    /// </summary>
    public void RaiseStock()
    {
        _stockPrice++;
        Data data = new Data(Command.Raise, _playerIndex, _stockPrice);
        string json = JsonUtility.ToJson(data);
        print($"Serialized. json: {json}");
        _turnManager.SendMove(json, true);
        _controlPanel.SetActive(false);
    }

    #region IPunTurnManagerCallbacks の実装
    void IPunTurnManagerCallbacks.OnTurnBegins(int turn)
    {
        Debug.Log($"Enter OnTurnBegins. turn: {turn}");

        if (turn == 1)
        {
            InitializeGame();
        }

        _activePlayerIndex = 0;    // 最初のプレイヤーからターンを始める
        PrepareControl();
    }

    void IPunTurnManagerCallbacks.OnPlayerMove(Player player, int turn, object move)
    {
        Debug.Log($"Enter OnPlayerMove. player: {player.ActorNumber}, turn: {turn}, move: {move.ToString()}");
    }

    void IPunTurnManagerCallbacks.OnPlayerFinished(Player player, int turn, object move)
    {
        Data data = JsonUtility.FromJson<Data>(move.ToString());
        ParseData(data);
        _activePlayerIndex = (_activePlayerIndex + 1) % PhotonNetwork.CurrentRoom.PlayerCount;
        PrepareControl();
    }

    void IPunTurnManagerCallbacks.OnTurnCompleted(int turn)
    {
        Debug.Log($"Enter OnTurnCompleted. turn: {turn}");

        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("I am MasterClient. I begin turn.");
            _turnManager.BeginTurn();
        }
    }

    void IPunTurnManagerCallbacks.OnTurnTimeEnds(int turn)
    {
        Debug.Log($"Enter OnTurnTimeEnds. turn: {turn}");
    }
    #endregion
}
