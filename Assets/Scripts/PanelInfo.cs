using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelInfo : MonoBehaviour
{
    RawMaterials material;

    private void Update()
    {
        Informations();
    }
    public void Informations()
    {
        if (gameObject.activeSelf)
        {
            Text[] texts = GetComponentsInChildren<Text>();
            texts[0].text = "Gold";
            texts[1].text = "Count: " + material.count;
        }
    }

    public void TurnOn(RawMaterials m)
    {
        gameObject.SetActive(true);
        material = m;
    }

    public void turnOff()
    {
        gameObject.SetActive(false);
    }
}
