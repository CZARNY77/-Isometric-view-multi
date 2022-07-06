using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance;
    [SerializeField] int myNumberInRoom;
    SpawnPoints[] spawnPoints;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        Instance = this;
        spawnPoints = GetComponentsInChildren<SpawnPoints>();
    }

    public Transform GetSpawnerpoint()
    {
        myNumberInRoom = 0;
        Player[] players = PhotonNetwork.PlayerList;
        foreach (Player p in players)
        {
            if (p != PhotonNetwork.LocalPlayer)
                myNumberInRoom++;
            else break;
        }
        return spawnPoints[myNumberInRoom].transform;
    }
}
