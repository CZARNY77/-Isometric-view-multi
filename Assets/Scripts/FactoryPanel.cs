using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System.IO;

public class FactoryPanel : MonoBehaviour
{
    Button btn;

    private void Start()
    {
        btn = GetComponentInChildren<Button>();
        btn.onClick.AddListener(FactoryManager.Instance.createMob);
    }
}
