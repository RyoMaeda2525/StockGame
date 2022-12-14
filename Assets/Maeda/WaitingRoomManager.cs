using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaitingRoomManager : MonoBehaviour
{
    [SerializeField]
    Text[] nicknameTexts;

    private void Start()
    {
        //NameSet();
    }

    public void NameSet()
    {
        Player[] players = PhotonNetwork.PlayerList;

        foreach (var text in nicknameTexts)
        {
            text.enabled = false;
        }

        for (int i = 0; i < players.Length; i++)
        {
            nicknameTexts[i].enabled = true;
            nicknameTexts[i].text = players[i].NickName;
        }
    }
}
