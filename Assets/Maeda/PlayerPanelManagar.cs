using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPanelManagar : MonoBehaviour
{
    [SerializeField, Tooltip("画面上に表示する資金のText")]
    Text _fundText = default;

    [SerializeField, Tooltip("画面上に出る4種の株価のText")]
    Text[] _stockTypes;

    /// <summary>
    /// 資金と持ち株の変動を受けとる
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
