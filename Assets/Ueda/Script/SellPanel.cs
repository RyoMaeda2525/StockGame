using System;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class SellPanel : MonoBehaviour
{
    int[] _quantity = new int[] { 1 };
    [SerializeField] Text _quantityText = null;
    [SerializeField] Text _checkText = null;
    [SerializeField] Text _priceText = null;
    int _myPlayerIndex = 0;
    int _playerIndex = 0;
    int _stockPrice = 0;
    int _totalPrice = 0;
    int _money = 0;
    [SerializeField] GameObject _system = null;
    [SerializeField] PlayerUIManager _playerUIManager = null;
    BoardManager _board = null;
    GameManager _gm = null;
    // Start is called before the first frame update
    void Start()
    {
        _board = _system.GetComponent<BoardManager>();
        _gm = _system.GetComponent<GameManager>();
        _myPlayerIndex = Array.IndexOf(PhotonNetwork.PlayerList, PhotonNetwork.LocalPlayer);
    }

    // Update is called once per frame
    void Update()
    {
        _quantityText.text = _quantity[0].ToString() + "個";//個数表示
        _stockPrice = _board.StockPrice(_playerIndex);//株価取得
        _totalPrice = _stockPrice * _quantity[0];
        _checkText.text = PhotonNetwork.PlayerList[_playerIndex].NickName + $"の株を{_quantity[0]}個 売却 ";//購入対象と個数の確認
        _priceText.text = _totalPrice.ToString(); //価格表示
    }

    public void SellButton()
    {
        Debug.Log($"{_playerIndex + 1}Pの株を{_quantity[0]}個 売却 合計 {_totalPrice} 円");
        _money = _playerUIManager.PlayerFundCheck(_myPlayerIndex);

        if (_gm.OtherPrice[_playerIndex] >= _quantity[0])
        {
            _gm.StockSell(_playerIndex, _stockPrice, _quantity);
            //ゲームマネージャーの株を売る関数に「対象プレイヤー」「対象の株価」「売却する数」を送る
            this.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("売却不可");
        }
        
    }




    //これより下、個数調整ボタン用スクリプト
    public void QuantityUp()
    {
        if (_quantity[0] < 99) _quantity[0]++;
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
