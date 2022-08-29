using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPanelManagar : MonoBehaviour
{
    [SerializeField, Tooltip("��ʏ�ɕ\�����鎑����Text")]
    Text _fundText = default;

    [SerializeField, Tooltip("��ʏ�ɏo��4��̊�����Text")]
    Text[] _stockTypes;

    /// <summary>
    /// �����Ǝ������̕ϓ����󂯂Ƃ�
    /// </summary>
    /// <param name="money"></param>
    /// <param name="stockType"></param>
    /// <param name="stockIndex"></param>
    public void MoneyAndStockChange(int money , int stockType , int stockIndex) 
    {
        _fundText.text = money.ToString();
        _stockTypes[stockType].text = stockIndex.ToString();
    }
}
