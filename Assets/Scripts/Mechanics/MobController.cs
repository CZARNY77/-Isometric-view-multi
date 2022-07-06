using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;

public class MobController : MonoBehaviour
{
    [SerializeField] bool working = false;
    bool selected = false;
    bool hover = false;
    Vector3 tempHitPoint;
    Ray ray;
    RaycastHit hit;

    public NavMeshAgent agent;
    [SerializeField] Material hoverMat;
    [SerializeField] Material selectMat;
    [SerializeField] Material normalMat;
    [SerializeField] GameObject Objective;
    GameObject tempObjective;
    RawMaterials mineral;
    PlayerController player;

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
                GetComponent<MobAnimatorMenager>().switchAnim(false);
                tempHitPoint = hit.point;
                if (tempObjective) Destroy(tempObjective);
                tempObjective = Instantiate(Objective, hit.point, Quaternion.Euler(90f, 0, 0));
                working = false;
            }
        }

        if (Vector3.Distance(tempHitPoint, transform.position) < 0.2f && tempObjective)
        {
            GetComponent<MobAnimatorMenager>().switchAnim(true);
            Destroy(tempObjective);
        }

        if(working && Vector3.Distance(tempHitPoint, transform.position) - agent.stoppingDistance < 0.2f)
        {
            player.countGold += 100;
            mineral.count -= 100;
            agent.SetDestination(FactoryManager.Instance.transform.position + new Vector3(0,0, 3f));
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

    public void dig(GameObject _mineral, GameObject _player)
    {
        agent.stoppingDistance = 2f;
        working = true;
        mineral = _mineral.GetComponent<RawMaterials>();
        player = _player.GetComponent<PlayerController>();
    }
}
