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
    /// <param name="fund"></param>
    /// <param name="stockType"></param>
    /// <param name="stockIndex"></param>
    public void FundAndStockChange(int stockType , int stockIndex , int fund) 
    {
        _fundText.text = fund.ToString();
        _stockTypes[stockType].text = stockIndex.ToString();
    }
}
