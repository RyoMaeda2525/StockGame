using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleInputField : MonoBehaviour
{
    [SerializeField] InputField _roomId = default;

    [SerializeField] InputField _nickName = default;

    [SerializeField] NetworkGameManagerTurnBased _ngmt = default;

    // Start is called before the first frame update
    void Start()
    {
        //InputField roomId = GetComponent<InputField>();
        //_ngmt = GetComponent<NetworkGameManagerTurnBased>();
    }

    public void RoomJoinOrCreate(int a)
    {
        if (_roomId.text != "" && _nickName.text != "")
        {
            SceneManager.LoadScene(a);
            _ngmt.OnJoinOrCreateRoom(_roomId.text, _nickName.text);
        }
        else Debug.Log("部屋名とニックネームを入力してください。");       
    }

    public void RandomJoinOrCreate(int a) 
    {
        if (_nickName.text != "")
        {
            SceneManager.LoadScene(a);
            _ngmt.JoinExistingRoom();
        }
        else Debug.Log("ニックネームを入力してください。");
    }

}
