using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

namespace Com.MyCompany.MyGame
{
    public class Luncher : MonoBehaviourPunCallbacks
    {
        string gameversion = "1";
        bool isConnecting;
        [SerializeField] private byte maxPlayersPerRoom = 2;
        [SerializeField] private GameObject controlPanel;
        [SerializeField] private GameObject progressLabel;



        private void Awake()
        {
            PhotonNetwork.AutomaticallySyncScene = true;
        }
        void Start()
        {
            progressLabel.SetActive(false);
            controlPanel.SetActive(true);
        }

        public override void OnConnectedToMaster()
        {
            if (isConnecting)
            {
                Debug.Log("OnConnectedTomaster()");
                PhotonNetwork.JoinRandomRoom();
                isConnecting = false;
            } 
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            base.OnDisconnected(cause);
            Debug.Log("OnDisconnected(" + cause + ")");
            progressLabel.SetActive(false);
            controlPanel.SetActive(true);
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            base.OnJoinRandomFailed(returnCode, message);
            Debug.Log("OnJoinRandomFailed(" + returnCode + ", " + message);
            PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayersPerRoom});
        }

        public override void OnJoinedRoom()
        {
            Debug.Log("Po³¹czy³em siê!");
            Debug.Log("Nick: " + PhotonNetwork.NickName);
            if(PhotonNetwork.CurrentRoom.PlayerCount == 1)
            {
                Debug.Log("Wszytuje do lobby!");
                PhotonNetwork.LoadLevel("1vs1");
            }
        }

        public void Connected()
        {
            progressLabel.SetActive(true);
            controlPanel.SetActive(false);

            if (PhotonNetwork.IsConnected)
            {
                PhotonNetwork.JoinRandomRoom();
                Debug.Log("Jest po³¹czenie");
            }
            else
            {
                isConnecting = PhotonNetwork.ConnectUsingSettings();
                PhotonNetwork.GameVersion = gameversion;
                Debug.Log("brak po³¹czenia");
            }
        }
    }
}
