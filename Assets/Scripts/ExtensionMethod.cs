using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtensionMethod
{
    public static Object Instantiate(this Object thisObj, Object orginal, Vector3 position, Quaternion rotation, Transform parent, string _userID)
    {
        GameObject timeBox = Object.Instantiate(orginal, position, rotation, parent) as GameObject;
        
    }
}
