using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void SceneIndexJump(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void SceneStringJump(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void RoomLeftTitleJump() 
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene(0);
    }
}
