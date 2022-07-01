using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using Zenject;

namespace Com.MyCompany.MyGame
{
    public class GameManager : MonoBehaviourPunCallbacks
    {
       /* public GameObject playerPrefab;

        private void Start()
        {
            if(playerPrefab == null) Debug.LogError("<Color=Red><a>Missing</a></Color> Nie podpiêto prefaba gracza", this);
            else
            {
                if (PlayerManager.LocalPlayerInstance == null)
                {
                    Debug.LogFormat("Wczytano gracza!");
                    PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0f, 5f, 0f), Quaternion.identity, 0);
                }
                else
                {
                    Debug.LogFormat("Ignoruje ³adowanie sceny dla {0}!", SceneManagerHelper.ActiveSceneName);
                }
            }
        }

        void LoadArena()
        {
            if (!PhotonNetwork.IsMasterClient)
            {
                Debug.Log("Nie mo¿ey wczytaæ mapy, bo zgubiliœmy Master'a");
            }
            PhotonNetwork.LoadLevel("1vs1");
        }

        public override void OnLeftRoom()
        {
            base.OnLeftRoom();
            SceneManager.LoadScene(0);
        }
        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            Debug.LogFormat("{0} do³¹czy³ do lobby", newPlayer.NickName);
            if (PhotonNetwork.IsMasterClient)
            {
                Debug.LogFormat("On playerEnteredRoom {0}", PhotonNetwork.IsMasterClient);
                LoadArena();
            }
        }
        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            Debug.LogFormat("{0} wyszed³ z lobby", otherPlayer.NickName);
            if (PhotonNetwork.IsMasterClient)
            {
                Debug.LogFormat("On playerLeftRoom {0}", PhotonNetwork.IsMasterClient);
                LoadArena();
            }
        }*/
    }
}
