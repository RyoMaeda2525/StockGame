using System;
using UnityEngine;

/// <summary>
/// 盤を管理するコンポーネント
/// </summary>
public class BoardManager : MonoBehaviour
{
    /// <summary>各プレイヤーの使う駒</summary>
    [SerializeField] Transform[] _marker;
    /// <summary>盤のルートとなるオブジェクト</summary>
    [SerializeField] Transform _tableRoot;
    /// <summary>盤</summary>
    RectTransform[] _priceTable;

    void Start()
    {
        // 盤のルートの子オブジェクトから、名前が "Price" で始まるものを配列に入れて奥
        _priceTable = _tableRoot.GetComponentsInChildren<RectTransform>();
        _priceTable = Array.FindAll(_priceTable, x => x.name.StartsWith("Price"));
    }

    /// <summary>
    /// 株価を変えるために駒を動かす
    /// </summary>
    /// <param name="targetPlayer">駒を動かすプレイヤーの index</param>
    /// <param name="price">いくらに移動するか</param>
    public void ChangeStockPrice(int targetPlayer, int price)
    {
        // 「いくらに移動するか」のターゲットとなるオブジェクト（アンカー）を探し、駒をその子オブジェクトにすることで移動させる
        var targetAnchor = Array.Find<RectTransform>(_priceTable, x => x.name == $"Price {targetPlayer} {price}");
        _marker[targetPlayer].transform.SetParent(targetAnchor.transform);
        _marker[targetPlayer].localPosition = Vector3.zero;
    }
}
