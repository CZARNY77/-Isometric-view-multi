using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class FactoryManager : MonoBehaviour
{
    PhotonView PV;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    private void Update()
    {
        if (!PV) return;
    }
}
