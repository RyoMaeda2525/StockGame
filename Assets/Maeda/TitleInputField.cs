using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleInputField : MonoBehaviour
{
    [SerializeField] InputField _roomId = default;

    [SerializeField] NetworkGameManagerTurnBased _ngmt = default;

    // Start is called before the first frame update
    void Start()
    {
        //InputField roomId = GetComponent<InputField>();
        //_ngmt = GetComponent<NetworkGameManagerTurnBased>();
    }

    public void RoomJoinOrCreate()
    {
        _ngmt.OnJoinOrCreateRoom(_roomId.text);
    }

}
