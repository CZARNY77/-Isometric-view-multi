using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;


public class PlayerListItem : MonoBehaviourPunCallbacks
{
    [SerializeField] Text playersList;
    Player nick;
 
    void Start()
    {
        playersList = gameObject.GetComponent<Text>();
    }

    public void SetUp(Player _nick)
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
}

