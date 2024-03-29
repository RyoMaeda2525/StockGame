using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPanelManagar : MonoBehaviour
{
    [SerializeField, Tooltip("株価を取得するのに使う")]
    BoardManager _boardManager;

    [SerializeField, Tooltip("画面上に表示する資金のText")]
    public Text _fundText = default;

    [SerializeField, Tooltip("画面上に出る4種の株価のText")]
    Text[] _stockTypes;

    const int judgeIndex = 50000;

    /// <summary>
    /// 資金と持ち株の変動を受けとる
    /// </summary>
    /// <param name="fund"></param>
    /// <param name="stockType"></param>
    /// <param name="stockIndex"></param>
    public void BuyStockChange(int stockType , int stockIndex) 
    {
        _fundText.text = (int.Parse(_fundText.text) - _boardManager.StockPrice(stockType) * stockIndex).ToString();
        _stockTypes[stockType].text = (int.Parse(_stockTypes[stockType].text) + stockIndex).ToString();
    }

    public bool SellStockChange(int stockType, int stockIndex)
    {
        _fundText.text = (int.Parse(_fundText.text) + _boardManager.StockPrice(stockType) * stockIndex).ToString();
        _stockTypes[stockType].text = (int.Parse(_stockTypes[stockType].text) - stockIndex).ToString();
        if (int.Parse(_fundText.text) >= judgeIndex) { return true; }
        return false;
    }

    public void FundAndStockSet(int stockType, int stockIndex , int fund)
    {
        _fundText.text = fund.ToString();
        _stockTypes[stockType].text = stockIndex.ToString();
    }
}
