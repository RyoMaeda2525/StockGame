using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyPanel : MonoBehaviour
{
    int[] _quantity = new int[] { 1 };
    [SerializeField] Text _quantityText = null;
    [SerializeField] Text _checkText = null;
    [SerializeField] Text _priceText = null;
    int _playerIndex =0;
    int _stockPrice = 0;
    [SerializeField] GameObject _system = null;
    BoardManager _board = null;
    GameManager _gm =null;
    // Start is called before the first frame update
    void Start()
    {
        _board = _system.GetComponent<BoardManager>();
        _gm = _system.GetComponent<GameManager>();
    }
    // Update is called once per frame
    void Update()
    {
        _quantityText.text = _quantity[0].ToString() +"個";//個数表示
        _stockPrice = _board.StockPrice(_playerIndex);//株価取得

        _checkText.text = $"{_playerIndex + 1}Pの株を{_quantity[0]}個 購入 ";//購入対象と個数の確認
        _priceText.text = _stockPrice.ToString(); //価格表示
    }

    public void BuyButton()
    {
        Debug.Log($"{_playerIndex+1}Pの株を{_quantity[0]}個 購入 ");
        _gm.StockBuy(_playerIndex,_stockPrice,_quantity);//ゲームマネージャーの株を買う関数に「対象プレイヤー」「対象の株価」「購入する数」を送る
    }




    //これより下、個数調整ボタン用スクリプト
    public void QuantityUp()
    {
        if (_quantity[0] <99) _quantity[0]++;
    }
    public void QuantityDown()
    {
        if (_quantity[0] > 1) _quantity[0]--;
    }
    //これより下、プレーヤー選択ボタン用スクリプト
    public void OnePlayer()
    {
        _playerIndex = 0;
    }
    public void TwoPlayer()
    {
        _playerIndex = 1;
    }
    public void ThreePlayer()
    {
        _playerIndex = 2;
    }
    public void FourPlayer()
    {
        _playerIndex = 3;
    }

}
