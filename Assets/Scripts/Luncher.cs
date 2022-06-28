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
            base.OnConnectedToMaster();
            Debug.Log("OnConnectedTomaster()");
            PhotonNetwork.JoinRandomRoom();
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
            base.OnJoinedRoom();
            Debug.Log("Do³¹czy³em do lobby!");
            //Debug.LogError("Nick: " + PhotonNetwork.NickName);
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
                PhotonNetwork.ConnectUsingSettings();
                PhotonNetwork.GameVersion = gameversion;
                Debug.Log("brak po³¹czenia");
            }
        }
    }
}
