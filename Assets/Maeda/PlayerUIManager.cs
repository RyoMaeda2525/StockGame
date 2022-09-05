using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerUIManager : MonoBehaviour
{
    public static PlayerUIManager instance;

    [SerializeField] GameManager _gameManager;

    [SerializeField, Tooltip("���͂��ꂽ�l���i�[����")]
    InputField _inputField;

    [SerializeField, Tooltip("�Q�[����ʂɂ͕\�����ꂸstring��n���̂Ɏg��Text")]
    Text _forSendingText = default;

    [SerializeField, Tooltip("�v���C���[�̃j�b�N�l�[��������z��")]
    Text[] _playerNickName;

    [SerializeField, Tooltip("�v���C���[�̏����󂯎��script�Q")]
    PlayerPanelManagar[] _playerTags;

    /// <summary>�v���C���[�̖��O���������������z��/// </summary>
    public Photon.Realtime.Player[] _playerArray;

    private void Awake()
    {
        Instance();
    }

    /// <summary>�ȑO�ɂ���΂������/// </summary>
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

    /// <summary>����,�ޏo���ɖ��O������֐�/// </summary>
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
