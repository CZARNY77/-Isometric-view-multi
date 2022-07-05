using Photon.Pun;
using Photon.Pun.Demo.PunBasics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerManager : MonoBehaviourPunCallbacks
{
    PhotonView PV;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    private void Start()
    {
        if (PV.IsMine)
        {
            CreateController();
        }
    }

    void CreateController()
    {
        Transform spawnpoint = SpawnManager.Instance.GetSpawnerpoint();
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Player"), spawnpoint.position, spawnpoint.rotation);
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Robot Kyle"), spawnpoint.position + new Vector3(0, -15, 0), spawnpoint.rotation);
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Factory"), spawnpoint.position + new Vector3(2, -15, 0), spawnpoint.rotation);
    }

}
