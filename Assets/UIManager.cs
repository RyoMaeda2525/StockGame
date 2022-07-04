using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Transform[] _marker;
    [SerializeField] Transform _tableRoot;
    Image[] _priceTable;

    void Start()
    {
        _priceTable = _tableRoot.GetComponentsInChildren<Image>();
    }

    public void RaiseStock(int targetPlayer, int price)
    {
        var targetImage = Array.Find<Image>(_priceTable, x => x.name == $"Price {targetPlayer} {price}");

        _marker[targetPlayer].transform.SetParent(targetImage.transform);
        _marker[targetPlayer].localPosition = Vector3.zero;
    }
}
