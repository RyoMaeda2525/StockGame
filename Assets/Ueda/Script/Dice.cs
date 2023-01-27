using UnityEngine;
using UnityEngine.UI;
public class Dice : MonoBehaviour
{
    [SerializeField] GameObject _dicePanel = null;
    [SerializeField] Animator[] _dice = null;
    [SerializeField] Text[] _player = null;
    [SerializeField] Text[] _result = null;
    [SerializeField] Text[] _total = null;

    private void Start() //確認用
    {
        int[] a = { 1, 3, 3, 6 };
        RollDice(a);
    }
    public void RollDice(int[] diceValue)
    {

        //_player[0] と _player[1] にプレイヤー名を表示するスクリプトを追加
        _dicePanel.SetActive(true);

        //_result[0] と _result[1] に結果に応じて「Win」「Lose」と表示するスクリプトを追加
        _total[0].text = (diceValue[0]+ diceValue[1]).ToString();
        _total[1].text = (diceValue[2] + diceValue[3]).ToString();

        for (int i = 0; i < diceValue.Length; i++)
        {
            switch(diceValue[i])//出た目に応じてアニメーションを再生
            {
                case 1:
                    _dice[i].SetTrigger("No1");
                    break;

                case 2:
                    _dice[i].SetTrigger("No2");
                    break;

                case 3:
                    _dice[i].SetTrigger("No3");
                    break;

                case 4:
                    _dice[i].SetTrigger("No4");
                    break;

                case 5:
                    _dice[i].SetTrigger("No5");
                    break;

                case 6:
                    _dice[i].SetTrigger("No6");
                    break;
            }
        }
        // panelのアニメーションが終わったら、 _dicePanel.SetActive(false);

    }
}
