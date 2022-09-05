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
    int[] _stockPrice;
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
        _stockPrice = new int[4];
        _stockPrice[0] = _initialStockPrice;
        _money = _initialMoney;
        _otherPrice = new int[4] {0,0,0,0};
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
                BuyStock(data.TargetPlayer,data.TargetPlayer,data.Value);
                break;

            case Command.Sell:
                SellStock(data.TargetPlayer, data.TargetPlayer, data.Value);
                break;


            case Command.Battle:
                BattleStock(data.TargetPlayer, data.TargetPlayer, data.Value);
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
    void ChangeStockPrice(int playerIndex, int[] targetPrice)
    {
        print($"Raise player {playerIndex}'s stock to {targetPrice}");
        _boardManager.ChangeStockPrice(playerIndex, targetPrice[0]);
    }

    /// <summary>
    /// 株を指定した分買う
    /// </summary>
    /// <param name="playerIndex">株を買ったプレイヤーの index</param>
    /// <param name="stockIndex">変動した持ち株の種類の index(まだ参照はしていない)</param>
    /// <param name="stock">この個数分買う</param>
    void BuyStock(int playerIndex, int stockIndex, int[] stock)
    {
        print($"player{playerIndex+1}は、player{stockIndex+1}の株を{stock[0]}個買った。");
    }
    /// <summary>
    /// 戦って株が減る
    /// </summary>
    /// <param name="targetIndex">対戦相手</param>
    /// <param name="winlose">勝ったか負けたか(1の時に自分の勝ち)</param>
    /// <param name="dise">それぞれのダイスの数</param>
    void BattleStock(int targetIndex, int winlose, int[] dise)
    {
        if(winlose == 1)//自分が勝った時
        {
            print($"player{_playerIndex}は、player{targetIndex}と戦い、" +
                $"player{_playerIndex}は{dise[0]}と{dise[1]}、" +
                $"player{targetIndex}は{dise[2]}と{dise[3]}を出し、" +
                $"結果、player{targetIndex}の株価が減りました。");
            _boardManager.ChangeStockPrice(targetIndex,_stockPrice[targetIndex]);
        }
        else//相手が勝った時
        {
            print($"player{_playerIndex}は、player{targetIndex}と戦い、" +
                $"player{_playerIndex}は{dise[0]}と{dise[1]}、" +
                $"player{targetIndex}は{dise[2]}と{dise[3]}を出し、" +
                $"結果、player{_playerIndex}の株価が減りました。");
            _boardManager.ChangeStockPrice(_playerIndex, _stockPrice[_playerIndex]);
        }
        
    }
    /// <summary>
    /// 持ち株を指定した分売る
    /// </summary>
    /// <param name="playerIndex">株価を変えたいプレイヤーの index</param>
    /// <param name="stockIndex">変動した持ち株の種類の index</param>
    /// <param name="changeStock">この値分株を売る</param>
    void SellStock(int playerIndex, int stockIndex, int[] Stock)
    {
        print($"player {playerIndex+1}は、player {stockIndex+1} の株を {Stock[0]}個売った");
    }

    /// <summary>
    /// 株価を 1 上げる
    /// ボタンから呼ばれる。PunTurnManager に Move (Finish) を送る。
    /// </summary>
    public void RaiseStock(bool finished = true)
    {
            _stockPrice[_playerIndex]+= 1;
            MoveStockPrice(true);
    }


    /// <summary>
    /// 指定した株を指定個数売る
    /// </summary>
    /// <param name="targetIndex">株の種類</param>
    /// <param name="targetStockPrice">その株の値段</param>
    /// <param name="StockNumber">売る個数</param>
    public void StockSell(int targetIndex, int targetStockPrice, int[] StockNumber)
    {
        _money += targetStockPrice * StockNumber[0];
        _otherPrice[targetIndex] -= StockNumber[0];

        MoveSellStock(targetIndex, StockNumber, true);
    }


    ///<summary>
    /// 戦う
    /// </summary>
    ///
    public void Battle(int targetIndex)
    {
        int[] dise = new int[4];
        for(int i = 0; i > dise.Length; i++)
        {
            dise[i] = UnityEngine.Random.Range(1,6);
        }
        if(dise[0] + dise[1] > dise[2] + dise[3])
        {
            _stockPrice[targetIndex] -= 2;
            MoveStockPrice2(targetIndex, dise, 1, true);
            //自分が勝った時のプログラム
        }
        else
        {
            _stockPrice[_playerIndex]--;
            MoveStockPrice2(targetIndex, dise, 0, true);
            //相手が勝った時のプログラム
        }

    }


    /// <summary>
    /// 株を指定数買う
    /// </summary>
    /// <param name="targetIndex">株の種類</param>
    /// <param name="targetStockPrice">その株の値段</param>
    /// <param name="StockNumber">個数</param>
    public void StockBuy(int targetIndex, int targetStockPrice, int[] StockNumber)
    {        
        _money = _money - targetStockPrice * StockNumber[0];
        _otherPrice[targetIndex] += StockNumber[0];
        MoveBuyStock(targetIndex, StockNumber, true);
    }
     
    /*public void BuyStock(bool finished = true)//次回、ここがboolじゃなくてintになる(相手のplayerIndexとその株の価格と買う個数)
    {
        _money = _money - _stockPrice[0];
        _otherPrice[_playerIndex]++;
        MoveBuyStock(true);
    }
    /// <summary>
    /// 指定した株を changeStock　の値分買う
    /// ボタンから呼ばれる。PunTurnManager に Move (Finish) を送る。
    /// </summary>
    //public void OnBuyStock(int stockIndex , int chageValue) 
    //{
    //    BuyStock(stockIndex , chageValue , true);
    //}
    */
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
    /// 自分、または相手の株価で PunTurnManager に Move　を送る
    /// </summary>
    /// <param name="finished">true の時は自分の番を終わる</param>
    void MoveStockPrice2(int targetIndex, int[] dise, int winlose, bool finished = true)
    {
        Data data = new Data(Command.Raise, targetIndex, winlose, dise);
        string json = JsonUtility.ToJson(data);
        print($"Serialized. json: {json}");
        _turnManager.SendMove(json, finished);
        _controlPanel.SetActive(!finished);
    }
    /// <summary>
    /// 現在の自分の株数で PunTurnManager に Move を送る
    /// </summary>
    /// <param name="finished">true の時は自分の番を終わる</param>
    void MoveBuyStock(int targetIndex, int[] BuyPrice, bool finished = true) 
    {
        Data data = new Data(Command.Buy, _playerIndex,targetIndex,BuyPrice);
        string json = JsonUtility.ToJson(data);
        print($"Serialized. json: {json}");
        _turnManager.SendMove(json, finished);
        _controlPanel.SetActive(!finished);
    }

    /// <summary>
    /// 現在の自分の株数で PunTurnManager に Move を送る
    /// </summary>
    /// <param name="finished">true の時は自分の番を終わる</param>
    void MoveSellStock(int targetIndex, int[] SellPrice, bool finished = true)
    {
        Data data = new Data(Command.Sell, _playerIndex, targetIndex, SellPrice);
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
