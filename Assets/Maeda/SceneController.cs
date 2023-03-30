using Photon.Pun;
using Photon.Pun.UtilityScripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviourPunCallbacks
{
    [SerializeField] PunTurnManager _turnManager;

    public void SceneIndexJump(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void SceneStringJump(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void GameStart(int index) 
    {
        PhotonNetwork.CurrentRoom.IsOpen = false;
        PhotonNetwork.LoadLevel(index);
        //_turnManager.BeginTurn();
    }

    public void RoomLeftTitleJump() 
    {
        Debug.Log(PhotonNetwork.LeaveRoom());
    }
}
