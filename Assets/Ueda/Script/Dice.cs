using UnityEngine;

public class Dice : MonoBehaviour
{
    [SerializeField] GameObject _dicePrefab = null;
    [SerializeField] Transform[] _diceTrans;
    // Start is called before the first frame update
    void Start()
    {
        
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
            GameObject dice = Instantiate( _dicePrefab,a,a);
            Animator anim = dice.GetComponent<Animator>();
        }
                

    }
}
