using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using Zenject;

public class GameManager : MonoBehaviourPunCallbacks
{

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            LeaveRoom();
        }
    }
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene("Luncher");
        MenuManager.Instance.OpenMenu("Loading");
    }
}
