﻿using System;
using UnityEngine;

/// <summary>
/// 盤を管理するコンポーネント
/// </summary>
public class BoardManager : MonoBehaviour
{
    [SerializeField] GameManager _gameManager;
    /// <summary>各プレイヤーの使う駒</summary>
    [SerializeField] Transform[] _marker;
    /// <summary>盤のルートとなるオブジェクト</summary>
    [SerializeField] Transform _tableRoot;
    /// <summary>盤</summary>
    RectTransform[] _priceTable;

    //void Start()
    //{
    //    // 盤のルートの子オブジェクトから、名前が "Price" で始まるものを配列に入れて奥
    //    _priceTable = _tableRoot.GetComponentsInChildren<RectTransform>();
    //    _priceTable = Array.FindAll(_priceTable, x => x.name.StartsWith("Price"));

    //    for (int i = 0; i < _marker.Length; i++) ChangeStockPrice(i, _gameManager._initialStockPrice);
    //}

    /// <summary>
    /// プレイヤーの人数に合わせて盤を作る 
    /// </summary>
    public void BoardRemake() 
    {
        // 盤のルートの子オブジェクトから、名前が "Price" で始まるものを配列に入れて奥
        _priceTable = _tableRoot.GetComponentsInChildren<RectTransform>();
        _priceTable = Array.FindAll(_priceTable, x => x.name.StartsWith("Price"));

        //for (int i = 0; i < _marker.Length; i++) ChangeStockPrice(i, _gameManager._initialStockPrice - 1);
    }

    /// <summary>
    /// 株価を変えるために駒を動かす
    /// </summary>
    /// <param name="targetPlayer">駒を動かすプレイヤーの index</param>
    /// <param name="price">いくらに移動するか</param>
    public void ChangeStockPrice(int targetPlayer, int price)
    {
        // 「いくらに移動するか」のターゲットとなるオブジェクト（アンカー）を探し、駒をその子オブジェクトにすることで移動させる
        var targetAnchor = Array.Find(_priceTable, x => x.name == $"Price {targetPlayer} {price}");
        _marker[targetPlayer].transform.position = targetAnchor.transform.position; //子オブジェクトでは無く位置に移動させている
    }

    private void StockPriceSearch(int targetPlayer)
    {
        foreach (var a in _priceTable)
        {
            if (_marker[targetPlayer].transform.position == a.transform.position)
            {
                var strings = a.name.Split(" ");
                Debug.Log(strings[2] + 1);
            }
        }
    }
    public int StockPrice(int targetPlayer)
    {
        int x = 0;
        foreach (var a in _priceTable)
        {
            
            if (_marker[targetPlayer].transform.position == a.transform.position)
            {
                var strings = a.name.Split(" ");
                x = (int.Parse(strings[2]) +1) * 1000;
                
            }
        }
        return x;
    }
}
