using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleInputField : MonoBehaviour
{
    [SerializeField] InputField _roomId = default;

    [SerializeField] InputField _nickName = default;

    [SerializeField] NetworkGameManagerTurnBased _ngmt = default;

    [SerializeField] SceneController sc;

    public void RoomJoinOrCreate()
    {
        if (_roomId.text != "" && _nickName.text != "")
        {
            sc.SceneIndexJump(1);
            _ngmt.OnJoinOrCreateRoom(_roomId.text, _nickName.text);
        }
        else Debug.Log("�������ƃj�b�N�l�[������͂��Ă��������B");       
    }

    public void RandomJoinOrCreate() 
    {
        if (_nickName.text != "")
        {
            _ngmt.JoinExistingRoom(_nickName.text);
            sc.SceneIndexJump(1);
        }
        else Debug.Log("�j�b�N�l�[������͂��Ă��������B");
    }

}
