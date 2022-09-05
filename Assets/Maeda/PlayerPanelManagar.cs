using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPanelManagar : MonoBehaviour
{
    [SerializeField, Tooltip("�������擾����̂Ɏg��")]
    BoardManager _boardManager;

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
    public void BuyStockChange(int stockType , int stockIndex) 
    {
        _fundText.text = (int.Parse(_fundText.text) - _boardManager.StockPrice(stockType) * stockIndex).ToString();
        _stockTypes[stockType].text = (int.Parse(_stockTypes[stockType].text) + stockIndex).ToString();
    }

    public void SellStockChange(int stockType, int stockIndex)
    {
        _fundText.text = (int.Parse(_fundText.text) + _boardManager.StockPrice(stockType) * stockIndex).ToString();
        _stockTypes[stockType].text = (int.Parse(_stockTypes[stockType].text) - stockIndex).ToString();
    }

    public void FundAndStockSet(int stockType, int stockIndex , int fund)
    {
        _fundText.text = fund.ToString();
        _stockTypes[stockType].text = stockIndex.ToString();
    }
}
