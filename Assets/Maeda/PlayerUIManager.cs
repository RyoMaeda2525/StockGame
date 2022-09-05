using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

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

    //int _stockIndex = -1;

    //void StockSelect(int value)
    //{
    //    _stockIndex = value;
    //}

    //void BuyOrSell() 
    //{
    //    int x = int.Parse(_inputField.text);
    //    if (x != 0) 
    //    {
    //        _forSendingText.text =  _stockIndex.ToString() + " " + x.ToString();
    //    } 
    //}

    public void PlayerInfoChange(int targetIndex, int stockType, int stockValue, int fund)
    {
        if (_playerTags[targetIndex].gameObject.activeSelf)
        {
            _playerTags[targetIndex].FundAndStockChange(stockType, stockValue, fund);
        }
    }

    /// <summary>入室,退出時に名前を入れる関数/// </summary>
    public void NameSet()
    {
        _playerArray = PhotonNetwork.PlayerList;

        for (int i = 0; i < _playerArray.Length; i++)
        {
            _playerNickName[i].text = _playerArray[i].NickName;
            Debug.Log(_playerArray[i].NickName);
        }
    }
}
