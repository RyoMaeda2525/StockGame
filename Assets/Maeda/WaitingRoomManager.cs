using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaitingRoomManager : MonoBehaviour
{
    [SerializeField]
    Text[] _nicknameTexts;

    [SerializeField , Header("ルーム名")]
    Text roomName;

    [SerializeField]
    GameObject _startButton;

    private void Start()
    {
        if(PhotonNetwork.RoomName != null) 
        {
            roomName.text = "ルーム名:"+PhotonNetwork.RoomName;
        }
        else roomName.text = "ランダムマッチ";
    }

    public void NameSet()
    {
        Player[] players = PhotonNetwork.PlayerList;

        foreach (var text in _nicknameTexts)
        {
            text.enabled = false;
        }

        for (int i = 0; i < players.Length; i++)
        {
            _nicknameTexts[i].enabled = true;
            _nicknameTexts[i].text = players[i].NickName;
        }

        if (PhotonNetwork.IsMasterClient && players.Length > 1) { _startButton.SetActive(true); }
        else { _startButton.SetActive(false);}
    }
}
