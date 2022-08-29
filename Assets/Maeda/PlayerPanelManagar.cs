using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPanelManagar : MonoBehaviour
{
    [SerializeField, Tooltip("‰æ–Êã‚É•\¦‚·‚é‘‹à‚ÌText")]
    Text _fundText = default;

    [SerializeField, Tooltip("‰æ–Êã‚Éo‚é4í‚ÌŠ”‰¿‚ÌText")]
    Text[] _stockTypes;

    /// <summary>
    /// ‘‹à‚Æ‚¿Š”‚Ì•Ï“®‚ğó‚¯‚Æ‚é
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
