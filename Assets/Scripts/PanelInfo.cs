using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelInfo : MonoBehaviour
{
    // Start is called before the first frame update
    public void Informations(int count)
    {
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
        else
        {
            Text[] texts = GetComponentsInChildren<Text>();
            texts[0].text = "Gold";
            texts[1].text = "Count: " + count;
            gameObject.SetActive(true);
        }
        
    }

    public void turnOff()
    {
        gameObject.SetActive(false);
    }
}
