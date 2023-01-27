using UnityEngine;
using UnityEngine.UI;
public class Dice : MonoBehaviour
{
    [SerializeField] GameObject _dicePanel = null;
    [SerializeField] Animator[] _dice = null;
    [SerializeField] Text[] _player = null;
    [SerializeField] Text[] _result = null;
    [SerializeField] Text[] _total = null;

    private void Start() //�m�F�p
    {
        int[] a = { 1, 3, 3, 6 };
        RollDice(a);
    }
    public void RollDice(int[] diceValue)
    {

        //_player[0] �� _player[1] �Ƀv���C���[����\������X�N���v�g��ǉ�
        _dicePanel.SetActive(true);

        //_result[0] �� _result[1] �Ɍ��ʂɉ����āuWin�v�uLose�v�ƕ\������X�N���v�g��ǉ�
        _total[0].text = (diceValue[0]+ diceValue[1]).ToString();
        _total[1].text = (diceValue[2] + diceValue[3]).ToString();

        for (int i = 0; i < diceValue.Length; i++)
        {
            switch(diceValue[i])//�o���ڂɉ����ăA�j���[�V�������Đ�
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
        // panel�̃A�j���[�V�������I�������A _dicePanel.SetActive(false);

    }
}
