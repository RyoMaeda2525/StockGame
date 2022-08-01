using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{
    [SerializeField] GameManager _gameManager;

    [SerializeField , Tooltip("���͂��ꂽ�l���i�[����")]
    InputField _inputField;

    [SerializeField , Tooltip("�Q�[����ʂɂ͕\�����ꂸstring��n���̂Ɏg��Text")]
    Text _forSendingText = default;

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
}
