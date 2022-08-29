using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{
    [SerializeField] GameManager _gameManager;

    [SerializeField , Tooltip("入力された値を格納する")]
    InputField _inputField;

    [SerializeField , Tooltip("ゲーム画面には表示されずstringを渡すのに使うText")]
    Text _forSendingText = default;

    [SerializeField, Tooltip("プレイヤーのニックネームを入れる配列")]
    Text[] _playerNickName;

    [SerializeField, Tooltip("プレイヤーの情報を受け取るscript群")]
    PlayerPanelManagar[] _playerTags;

    int _stockIndex = -1;

    void StockSelect(int value)
    {
        _stockIndex = value;
    }

    void BuyOrSell() 
    {
        int x = int.Parse(_inputField.text);
        if (x != 0) 
        {
            _forSendingText.text =  _stockIndex.ToString() + " " + x.ToString();
        } 
    }

    public void PlayerInfoChange(int targetIndex , int stockType, int stockValue , int fund) 
    {
        if (_playerTags[targetIndex].gameObject.activeSelf) 
        {
            _playerTags[targetIndex].FundAndStockChange(stockType, stockValue, fund);
        }
    }

    //public void NameSet() 
    //{
    //    var playerList = PhotonNetwork.PlayerList;

    //    for (int i = 0;)
    //}
}
