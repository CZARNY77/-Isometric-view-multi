using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MobController : MonoBehaviour
{
    Camera cam;
    bool moving = false;
    public NavMeshAgent agent;
    public string ownerID;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && cam != null)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
                agent.SetDestination(hit.point);
        }

    }
    public void setParameters(Camera _cam)
    {
        cam = _cam;
    }
}
