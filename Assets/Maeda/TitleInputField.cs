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
        else Debug.Log("部屋名とニックネームを入力してください。");       
    }

    public void RandomJoinOrCreate() 
    {
        if (_nickName.text != "")
        {
            _ngmt.JoinExistingRoom(_nickName.text);
            sc.SceneIndexJump(1);
        }
        else Debug.Log("ニックネームを入力してください。");
    }

}
