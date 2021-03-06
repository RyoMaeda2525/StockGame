using System;
using UnityEngine;
using UnityEngine.UI;
// Photon 用の名前空間を参照する
using Photon.Pun;
using Photon.Pun.UtilityScripts;    // PunTurnManager, IPunTurnManagerCallbacks を使うため
using Photon.Realtime;


/// <summary>
/// ゲーム・ターンを管理するコンポーネント
/// </summary>
public class GameManager : MonoBehaviour, IPunTurnManagerCallbacks
{ 
    [SerializeField] PunTurnManager _turnManager;
    [SerializeField] BoardManager _boardManager;
    /// <summary>操作をするためのパネル (UI)</summary>
    [SerializeField] GameObject _controlPanel;
    /// <summary>株価の初期値</summary>
    [SerializeField] int _initialStockPrice = 2;
    ///<summary> 資産の初期値</summary>
    [SerializeField] int _initialMoney = 30;
    /// <summary>プレイヤーの index。自分が何番目のプレイヤーかを表す。0スタートであり途中抜けを考慮していない。</summary>
    int _playerIndex = -1;
    /// <summary>現在何番目のプレイヤーが操作をしているか（0スタート。途中抜けを考慮していない）</summary>
    int _activePlayerIndex = -1;
    /// <summary>現在の自分の株価</summary>
    int _stockPrice;
    /// <summary>自分の資産(_money/千円)</summary>
    int _money;
    /// <summary>株の所持数(他プレイヤー株と種類を分けて記録)</summary>
    int[] _otherPrice;
    void Start()
    {
        _controlPanel.SetActive(false);
    }
    
    /// <summary>
    /// ゲームを初期化する
    /// </summary>
    void InitializeGame()
    {
        Debug.Log("Initialize Game...");
        _playerIndex = Array.IndexOf(PhotonNetwork.PlayerList, PhotonNetwork.LocalPlayer);
        _stockPrice = _initialStockPrice;
        _money = _initialMoney;
        _otherPrice = new int[] { 0, 0, 0, 0 };
        _otherPrice[_playerIndex] = 5;
    }

    /// <summary>
    /// 自分の番であるかどうかを取得する
    /// </summary>
    /// <returns>自分の番の場合は true</returns>
    bool IsMyTurn()
    {
        return _activePlayerIndex == _playerIndex;
    }

    /// <summary>
    /// コントロールパネルの表示・非表示を切り替える。
    /// 自分の番の場合は表示し、そうでない場合は非表示にする。
    /// </summary>
    void PrepareControl()
    {
        _controlPanel.SetActive(IsMyTurn());
    }

    /// <summary>
    /// 送られてきたデータをパースし、適切な処理に渡す。
    /// </summary>
    /// <param name="data">送られてきたデータ</param>
    void ParseData(Data data)
    {
        switch (data.Command)
        {
            case Command.Raise:
                ChangeStockPrice(data.TargetPlayer, data.Value);
                break;

            case Command.Buy:

                break;

            case Command.Sell:
                
            break;
            default:
                Debug.LogError($"Invalid command: {data.Command.ToString()}");
                break;
        }
    }

    /// <summary>
    /// 株価を指定した値に変更する
    /// </summary>
    /// <param name="playerIndex">株価を変えたいプレイヤーの index</param>
    /// <param name="targetPrice">この値に株価を変更する</param>
    void ChangeStockPrice(int playerIndex, int targetPrice)
    {
        print($"Raise player {playerIndex}'s stock to {targetPrice}");
        _boardManager.ChangeStockPrice(playerIndex, targetPrice);
    }

    /// <summary>
    /// 株を指定した分買う
    /// </summary>
    /// <param name="playerIndex">株価を変えたいプレイヤーの index</param>
    /// <param name="stockIndex">変動した持ち株の種類の index</param>
    /// <param name="changeStock">この値分株を買う</param>
    void BuyStock(int playerIndex, int stockIndex, int changeStock)
    {
        print($"Buy player {playerIndex}'s {stockIndex} stock to {changeStock}");
    }

    /// <summary>
    /// 持ち株を指定した分売る
    /// </summary>
    /// <param name="playerIndex">株価を変えたいプレイヤーの index</param>
    /// <param name="stockIndex">変動した持ち株の種類の index</param>
    /// <param name="changeStock">この値分株を売る</param>
    void SellStock(int playerIndex, int stockIndex, int changeStock)
    {
        print($"Sell player {playerIndex}'s {stockIndex} stock to {changeStock}");
    }

    /// <summary>
    /// 株価を 1 上げる
    /// ボタンから呼ばれる。PunTurnManager に Move (Finish) を送る。
    /// </summary>
    public void RaiseStock(bool finished = true)
    {
            _stockPrice++;
            MoveStockPrice(true);
    }

    /// <summary>
    /// 指定した株を changeStock　の値分買う
    /// ボタンから呼ばれる。PunTurnManager に Move (Finish) を送る。
    /// </summary>
    public void OnBuyStock(int stockIndex , int chageValue) 
    {
        BuyStock(stockIndex , chageValue , true);
    }

    /// <summary>
    /// 現在の自分の株価で PunTurnManager に Move を送る
    /// </summary>
    /// <param name="finished">true の時は自分の番を終わる</param>
    void MoveStockPrice(bool finished = true)
    {
        Data data = new Data(Command.Raise , _playerIndex , 0  , _stockPrice);
        string json = JsonUtility.ToJson(data);
        print($"Serialized. json: {json}");
        _turnManager.SendMove(json, finished);
        _controlPanel.SetActive(!finished);
    }

    /// <summary>
    /// 現在の自分の株価で PunTurnManager に Move を送る
    /// </summary>
    /// <param name="finished">true の時は自分の番を終わる</param>
    void BuyStock(int stockIndex , int value , bool finished = true) 
    {
        Data data = new Data(Command.Buy, _playerIndex, stockIndex , value);
        string json = JsonUtility.ToJson(data);
        print($"Serialized. json: {json}");
        _turnManager.SendMove(json, finished);
        _controlPanel.SetActive(!finished);
    }

    #region IPunTurnManagerCallbacks の実装
    void IPunTurnManagerCallbacks.OnTurnBegins(int turn)
    {
        Debug.Log($"Enter OnTurnBegins. turn: {turn}");

        if (turn == 1)
        {
            InitializeGame();
            MoveStockPrice(false);
        }

        _activePlayerIndex = 0;    // 最初のプレイヤーからターンを始める
        PrepareControl();
    }

    void IPunTurnManagerCallbacks.OnPlayerMove(Player player, int turn, object move)
    {
        Debug.Log($"Enter OnPlayerMove. player: {player.ActorNumber}, turn: {turn}, move: {move.ToString()}");
        Data data = JsonUtility.FromJson<Data>(move.ToString());
        ParseData(data);
    }

    void IPunTurnManagerCallbacks.OnPlayerFinished(Player player, int turn, object move)
    {
        Debug.Log($"Enter OnPlayerFinished. player: {player.ActorNumber}, turn: {turn}, move: {move.ToString()}");
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
