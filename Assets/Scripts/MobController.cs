using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;

public class MobController : MonoBehaviour
{
    public NavMeshAgent agent;
    Vector3 tempHitPoint;
    bool selected = false;
    bool hover = false;
    GameObject tempObjective;
    Ray ray;
    RaycastHit hit;


    [SerializeField] Material hoverMat;
    [SerializeField] Material selectMat;
    [SerializeField] Material normalMat;
    [SerializeField] GameObject Objective;

    PhotonView PV;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }


    void Update()
    {
        if (!PV.IsMine)
            return;


        if (hover && !selected)
        {
            changeMaterial(0);
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                changeMaterial(1);
                selected = true;
            }
        }
        else if(!selected || (Input.GetKeyDown(KeyCode.Mouse0) && !hover))
        {
            changeMaterial(2);
            selected = false;
        }

        if (Input.GetKeyDown(KeyCode.Mouse1) && selected)
        {
            if (Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);
                tempHitPoint = hit.point;
                if(tempObjective) Destroy(tempObjective);
                tempObjective = Instantiate(Objective, hit.point + new Vector3(0, 0.1f, 0), Quaternion.Euler(90f, 0, 0));
            }
        }

        if (Vector3.Distance(tempHitPoint, transform.position) < 0.2f && tempObjective)
        {
            Destroy(tempObjective);
        }
    }

    public void setParameters(bool _hover, Ray _ray, RaycastHit _hit)
    {
        hover = _hover;
        ray = _ray;
        hit = _hit;
    }

    public void changeMaterial(int state)
    {
        switch (state)
        {
            case 0:
                GetComponentInChildren<SkinnedMeshRenderer>().material = hoverMat;
                break;
            case 1:
                GetComponentInChildren<SkinnedMeshRenderer>().material = selectMat;
                break;
            default:
                GetComponentInChildren<SkinnedMeshRenderer>().material = normalMat;
                break;
        }
    }
}
