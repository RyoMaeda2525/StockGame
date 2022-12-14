using UnityEngine;

public class Dice : MonoBehaviour
{
    [SerializeField] GameObject _dicePrefab = null;
    [SerializeField] Transform[] _diceTrans;

    public void RollDice(int[] diceValue)
    {
        for(int i =4; i< 4;i++)
        {
            GameObject dice = Instantiate( _dicePrefab,_diceTrans[i]);//サイコロを生成
            Animator anim = dice.GetComponent<Animator>();
            switch(diceValue[i])//出た目に応じてアニメーションを再生
            {
                case 1:
                    anim.SetTrigger("No1");
                    break;

                case 2:
                    anim.SetTrigger("No2");
                    break;

                case 3:
                    anim.SetTrigger("No3");
                    break;

                case 4:
                    anim.SetTrigger("No4");
                    break;

                case 5:
                    anim.SetTrigger("No5");
                    break;

                case 6:
                    anim.SetTrigger("No6");
                    break;
            }
        }
                

    }
}
