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

    /// <summary>
    /// ����̃v���C���[���������z���擾����֐�
    /// </summary>
    /// <param name="playerIndex">�擾�������v���C���[��Index</param>
    /// <returns></returns>
    public int PlayerFundCheck(int playerIndex) 
    {
        string st = _playerTags[playerIndex]._fundText.text;
        return int.Parse(st);
    }

    /// <summary>
    /// ���𔃂����ۂ�
    /// �v���C���[�̎����⎑����\������֐�
    /// </summary>
    /// <param name="targetIndex">�ύX����v���C���[��Index</param>
    /// <param name="stockType">�ύX���銔��Index</param>
    /// <param name="stockValue">�ύX���銔�̒l</param>
    public void BuyStockChange(int targetIndex, int stockType, int stockValue)
    {
        if (_playerTags[targetIndex].gameObject.activeSelf)
        {
            _playerTags[targetIndex].BuyStockChange(stockType, stockValue);
        }
    }

    /// <summary>
    /// ���𔄂����ۂ�
    /// �v���C���[�̎����⎑����\������֐�
    /// </summary>
    /// <param name="targetIndex">�ύX����v���C���[��Index</param>
    /// <param name="stockType">�ύX���銔��Index</param>
    /// <param name="stockValue">�ύX���銔�̒l</param>
    public void SellStockChange(int targetIndex, int stockType, int stockValue)
    {
        if (_playerTags[targetIndex].gameObject.activeSelf)
        {
            _playerTags[targetIndex].SellStockChange(stockType, stockValue);
        }
    }

    /// <summary>
    /// �v���C���[�̎����⎑�����㏑������֐�
    /// </summary>
    /// <param name="targetIndex">�ύX����v���C���[��Index</param>
    /// <param name="stockType">�ύX���銔��Index</param>
    /// <param name="stockValue">�ύX���銔�̒l</param>
    /// <param name="fund">�v���C���[�̎����z</param>
    public void PlayerInfoSets(int targetIndex, int stockType, int stockValue , int fund)
    {
        if (_playerTags[targetIndex].gameObject.activeSelf)
        {
            _playerTags[targetIndex].FundAndStockSet(stockType, stockValue , fund);
        }
    }

    /// <summary>����,�ޏo���ɖ��O������֐�/// </summary>
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
