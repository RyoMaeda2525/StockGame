using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GridLayoutGroup))]
public class BoardGrid : MonoBehaviour
{
    GridLayoutGroup _gridLayoutGroup = default;

    [SerializeField]
    BoardManager _boardManager = default;

    [SerializeField, Tooltip("盤に配置するマスｓ")]
    GameObject _price = default;

    [SerializeField, Tooltip("株価を表すマス")]
    GameObject _priceNumber = default;

    [SerializeField, Tooltip("盤の縦マス")]
    int _row = 2;

    [SerializeField, Tooltip("盤の横マス")]
    int _column = 5;

    private void Start()
    {
        _gridLayoutGroup = GetComponent<GridLayoutGroup>();
        _gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedRowCount;
        _gridLayoutGroup.constraintCount = _row;

        for (int i = _row; i > 0; i--)
        {
            GameObject priceNumber =  Instantiate(_priceNumber, transform);
            Text text = priceNumber.transform.Find("Text").gameObject.GetComponent<Text>();
            text.text = i.ToString();
        }

        for (int c = _column - 1; c >= 0; c--)
        {
            for (int r = _row - 1; r >= 0; r--)
            {
                GameObject price = Instantiate(_price, transform);
                price.name = $"Price {c} {r}";
            }
        }

        _boardManager.BoardRemake();
    }
}
