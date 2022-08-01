using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyPanel : MonoBehaviour
{
    int quantity = 1;
    [SerializeField] Text quantityText = null;
    [SerializeField] Text checkText = null;
    int _playerIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        quantityText.text = quantity.ToString() +"��";
        if(_playerIndex ==0)
        {
            checkText.text = "�N" + " �̊���" + quantity.ToString() + " �� �w��";
        }
        else
        {
            checkText.text = $"{_playerIndex+1}P�̊���{quantity}�� �w�� ";
        }
    }

    public void BuyButton()
    {
        Debug.Log($"{_playerIndex+1}P�̊���{quantity}�� �w�� ");
    }




    //�����艺�A���l�����{�^��
    public void QuantityUp()
    {
        if (quantity <99) quantity++;
    }
    public void QuantityDown()
    {
        if (quantity > 1) quantity--;
    }
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
