using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class FactoryManager : MonoBehaviour
{
    public static FactoryManager Instance;
    PhotonView PV;

    private void Awake()
    {
        Instance = this;
        PV = GetComponent<PhotonView>();
    }

    private void Update()
    {
        if (!PV) return;
    }

    public void toSwitch(GameObject factoryPanel)
    {
        if (!PV) return;

        if (factoryPanel.activeSelf)
        {
            factoryPanel.SetActive(false);
        }
        else
        {
            factoryPanel.SetActive(true);
        }
    }
/*    public void turnOff()
    {
        factoryPanel.SetActive(false);
    }*/
    public void createMob()
    {
        Transform factoryTransform = gameObject.transform;
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Robot Kyle"), factoryTransform.position + new Vector3(0, 0, 3f), factoryTransform.rotation);
    }
}
