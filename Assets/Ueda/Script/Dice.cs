using UnityEngine;

public class Dice : MonoBehaviour
{
    [SerializeField] GameObject _dicePrefab = null;
    [SerializeField] Transform[] _diceTrans;
    // Start is called before the first frame update
    void Start()
    {
        RollDice();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void RollDice()
    {
        foreach(Transform a in _diceTrans)
        {
            int diceNum = Random.Range (1,7) ;
            GameObject dice = Instantiate( _dicePrefab,a);
            Animator anim = dice.GetComponent<Animator>();
            switch(diceNum)
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
