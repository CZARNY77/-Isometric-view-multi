using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

namespace Com.MyCompany.MyGame
{
    public class PlayersList : MonoBehaviourPunCallbacks
    {
        Text playersList;
        Player nick;
        [SerializeField] Text playerListPrefab;
        [SerializeField] Transform playerListTransform;
        void Start()
        {
            playersList = gameObject.GetComponent<Text>();
        }

        void SetUp(Player _nick)
        {
            nick = _nick;
            playersList.text = _nick.NickName;
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            if(nick == otherPlayer)
            {
                Destroy(gameObject);
            }
        }

        public override void OnLeftRoom()
        {
            Destroy(gameObject);
        }
        public void UpdateList()
        {
            /*playersList.text = "Players:\n";
            foreach (Player nick in PhotonNetwork.PlayerList)
                playersList.text += nick.NickName + "\n";*/
            foreach (Player name in PhotonNetwork.PlayerList)
                Instantiate(playerListPrefab, playerListTransform).GetComponent<PlayersList>().SetUp(name);

        }
    }
}
