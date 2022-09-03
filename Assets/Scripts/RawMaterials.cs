using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RawMaterials : MonoBehaviour
{
    public int count = 300;

    public void dig()
    {
        count -= 100;

        if(count <= 0)
        {
            Destroy(gameObject);
        }
    }
}