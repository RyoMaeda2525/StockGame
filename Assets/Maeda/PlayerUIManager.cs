using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using System;

public class PlayerUIManager : MonoBehaviour
{
    public static PlayerUIManager instance;

    [SerializeField] GameManager _gameManager;

    [SerializeField, Tooltip("入力された値を格納する")]
    InputField _inputField;

    [SerializeField, Tooltip("ゲーム画面には表示されずstringを渡すのに使うText")]
    Text _forSendingText = default;

    [SerializeField, Tooltip("プレイヤーのニックネームを入れる配列")]
    Text[] _playerNickName;

    [SerializeField, Tooltip("プレイヤーの情報を受け取るscript群")]
    PlayerPanelManagar[] _playerTags;

    /// <summary>プレイヤーの名前や入室順が分かる配列/// </summary>
    public Photon.Realtime.Player[] _playerArray;

    private void Awake()
    {
        Instance();
    }

    /// <summary>以前にあればこれを壊す/// </summary>
    private void Instance()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// 特定のプレイヤーが持つ資金額を取得する関数
    /// </summary>
    /// <param name="playerIndex">取得したいプレイヤーのIndex</param>
    /// <returns></returns>
    public int PlayerFundCheck(int playerIndex) 
    {
        string st = _playerTags[playerIndex]._fundText.text;
        return int.Parse(st);
    }

    /// <summary>
    /// 株を買った際に
    /// プレイヤーの持つ株や資金を表示する関数
    /// </summary>
    /// <param name="targetIndex">変更するプレイヤーのIndex</param>
    /// <param name="stockType">変更する株のIndex</param>
    /// <param name="stockValue">変更する株の値</param>
    public void BuyStockChange(int targetIndex, int stockType, int stockValue)
    {
        if (_playerTags[targetIndex].gameObject.activeSelf)
        {
            _playerTags[targetIndex].BuyStockChange(stockType, stockValue);
        }
    }

    /// <summary>
    /// 株を売った際に
    /// プレイヤーの持つ株や資金を表示する関数
    /// </summary>
    /// <param name="targetIndex">変更するプレイヤーのIndex</param>
    /// <param name="stockType">変更する株のIndex</param>
    /// <param name="stockValue">変更する株の値</param>
    public void SellStockChange(int targetIndex, int stockType, int stockValue)
    {
        if (_playerTags[targetIndex].gameObject.activeSelf)
        {
            _playerTags[targetIndex].SellStockChange(stockType, stockValue);
        }
    }

    /// <summary>
    /// プレイヤーの持つ株や資金を上書きする関数
    /// </summary>
    /// <param name="targetIndex">変更するプレイヤーのIndex</param>
    /// <param name="stockType">変更する株のIndex</param>
    /// <param name="stockValue">変更する株の値</param>
    /// <param name="fund">プレイヤーの資金額</param>
    public void PlayerInfoSets(int targetIndex, int stockType, int stockValue , int fund)
    {
        if (_playerTags[targetIndex].gameObject.activeSelf)
        {
            _playerTags[targetIndex].FundAndStockSet(stockType, stockValue , fund);
        }
    }

    /// <summary>入室,退出時に名前を入れる関数/// </summary>
    public void NameSet()
    {
        _playerArray = PhotonNetwork.PlayerList;

        for (int i = 0; i < _playerArray.Length; i++)
        {
            if (_playerNickName[i] != null) 
            {
                _playerNickName[i].text = _playerArray[i].NickName;
            }
        }
    }
}
