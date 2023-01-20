using System;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class BuyPanel : MonoBehaviour
{
    int[] _quantity = new int[] { 1 };
    [SerializeField] Text _quantityText = null;
    [SerializeField] Text _checkText = null;
    [SerializeField] Text _priceText = null;
    int _myPlayerIndex =0;
    int _playerIndex =0;
    int _stockPrice = 0;
    int _totalPrice = 0;
    int _money = 0;
    [SerializeField] GameObject _system = null;
    [SerializeField] PlayerUIManager _playerUIManager = null;
    BoardManager _board = null;
    GameManager _gm =null;
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
        _quantityText.text = _quantity[0].ToString() +"��";//���\��
        _stockPrice = _board.StockPrice(_playerIndex);//�����擾
        _totalPrice = _stockPrice * _quantity[0];
        _checkText.text = PhotonNetwork.PlayerList[_playerIndex].NickName + $"�̊���{_quantity[0]}�� �w�� ";//�w���Ώۂƌ��̊m�F
        _priceText.text = _totalPrice.ToString(); //���i�\��
       
    }

    public void BuyButton()
    {
        Debug.Log($"{_playerIndex+1}P�̊���{_quantity[0]}�� �w�� ���v {_stockPrice} �~");
        _money = _playerUIManager.PlayerFundCheck(_myPlayerIndex);
        if (_money >= _totalPrice)
        {
            _gm.StockBuy(_playerIndex, _stockPrice, _quantity);
            //�Q�[���}�l�[�W���[�̊��𔃂��֐��Ɂu�Ώۃv���C���[�v�u�Ώۂ̊����v�u�w�����鐔�v�𑗂�
            this.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("�w���s��");
        }
        
    }




    //�����艺�A�������{�^���p�X�N���v�g
    public void QuantityUp()
    {
        if (_quantity[0] <99) _quantity[0]++;
    }
    public void QuantityDown()
    {
        if (_quantity[0] > 1) _quantity[0]--;
    }
    //�����艺�A�v���[���[�I���{�^���p�X�N���v�g
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
