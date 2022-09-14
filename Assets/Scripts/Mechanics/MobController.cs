using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;
using UnityEngine.UI;

enum Location
{
    magazine,
    rawMaterial
}

public class MobController : MonoBehaviour
{
    [SerializeField] bool working = false;
    [SerializeField] bool walking = false;
    bool selected = false;
    bool hover = false;
    Vector3 tempHitPoint;
    Ray ray;
    RaycastHit hit;
    NavMeshHit navMeshHit;

    public NavMeshAgent agent;
    [SerializeField] Material hoverMat;
    [SerializeField] Material selectMat;
    [SerializeField] Material normalMat;
    [SerializeField] GameObject Objective;
    GameObject tempObjective;
    RawMaterials mineral;
    PlayerController player;

    PhotonView PV;

    Location goTo;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }


    void Update()
    {
        if (!PV.IsMine)
            return;

        mobMarking();
        mobMoving();
        mobWorking();
    }

    void mobMarking()
    {
        if (hover && !selected)
        {
            changeMaterial(0);
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                changeMaterial(1);
                selected = true;
            }
        }
        else if (!selected || (Input.GetKeyDown(KeyCode.Mouse0) && !hover))
        {
            changeMaterial(2);
            selected = false;
        }
    }

    void mobMoving()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && selected)
        {
            if (Physics.Raycast(ray, out hit))
            {
                NavMesh.SamplePosition(hit.point, out navMeshHit, 10.0f, NavMesh.AllAreas);
                tempHitPoint = navMeshHit.position;

                move(tempHitPoint, false);
                if (tempObjective) Destroy(tempObjective);
                tempObjective = Instantiate(Objective, tempHitPoint, Quaternion.Euler(90f, 0, 0));
                walking = false;

            }
        }
        // usuwanie znacznika jak jest blisko znacznika
        if (Vector3.Distance(tempHitPoint, transform.position) < 0.2f && tempObjective)
        {
            GetComponent<MobAnimatorMenager>().switchAnim(true);
            Destroy(tempObjective);
        }
    }

    void move(Vector3 tempHitPoint, bool stand)
    {
        agent.SetDestination(tempHitPoint);
        GetComponent<MobAnimatorMenager>().switchAnim(stand);
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

    public void dig(GameObject _mineral, GameObject _player)
    {
        walking = true;
        mineral = _mineral.GetComponent<RawMaterials>();
        player = _player.GetComponent<PlayerController>();
        goTo = Location.rawMaterial;
    }

    void mobWorking()
    {
        if (walking && !working)
        {
            if(goTo == Location.rawMaterial && mineral)
            {
                NavMesh.SamplePosition(mineral.gameObject.transform.position, out navMeshHit, 10.0f, NavMesh.AllAreas);
                tempHitPoint = navMeshHit.position;
                if(Vector3.Distance(tempHitPoint, transform.position) - agent.stoppingDistance < 0.2f)
                {
                    working = true;
                    Invoke("Working", 2.0f);
                   
                }
            }
            else if(goTo == Location.magazine)
            {
                NavMesh.SamplePosition(FactoryManager.Instance.transform.position, out navMeshHit, 10.0f, NavMesh.AllAreas);
                tempHitPoint = navMeshHit.position;
                if (Vector3.Distance(tempHitPoint, transform.position) - agent.stoppingDistance < 0.2f)
                {
                    working = true;
                    Invoke("Working", 2.0f);
                }
            }

            move(tempHitPoint, working);
        }
    }
        
    void Working()
    {
        working = false;

        if(goTo == Location.rawMaterial)
        {
            goTo = Location.magazine;
            mineral.count -= 100;
            if (mineral.count <= 0)
            {
                Destroy(mineral.gameObject);
            }
        }
        else if(goTo == Location.magazine)
        {
            if (mineral)
            {
                goTo = Location.rawMaterial;
            }
            else
            {
                walking = false;
            }
            player.countGold += 100;
            player.myCanvas.GetComponentInChildren<Text>().text = player.countGold.ToString();
        }
    }
}