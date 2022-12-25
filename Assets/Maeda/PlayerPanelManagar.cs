using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPanelManagar : MonoBehaviour
{
    [SerializeField, Tooltip("Š”‰¿‚ğæ“¾‚·‚é‚Ì‚Ég‚¤")]
    BoardManager _boardManager;

    [SerializeField, Tooltip("‰æ–Êã‚É•\¦‚·‚é‘‹à‚ÌText")]
    public Text _fundText = default;

    [SerializeField, Tooltip("‰æ–Êã‚Éo‚é4í‚ÌŠ”‰¿‚ÌText")]
    Text[] _stockTypes;

    const int judgeIndex = 50000;

    /// <summary>
    /// ‘‹à‚Æ‚¿Š”‚Ì•Ï“®‚ğó‚¯‚Æ‚é
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
