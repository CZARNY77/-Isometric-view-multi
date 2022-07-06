using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class PlayerController : MonoBehaviour
{
    CharacterController characterController;
    Vector3 moveDirection;
    Camera cam;
    PhotonView PV;

    [SerializeField] float speed;
    [SerializeField] Canvas myCanvas;
    PanelInfo panelInfo;
    FactoryPanel factoryPanel;

    GameObject tempObject;
    LayerMask maskMob;
    LayerMask maskMineral;
    LayerMask maskFactory;

    public int countGold = 100;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        cam = GetComponentInChildren<Camera>();
        characterController.detectCollisions = false;
        PV = GetComponentInChildren<PhotonView>();
        maskMob = LayerMask.GetMask("Mob");
        maskMineral = LayerMask.GetMask("Minerals");
        maskFactory = LayerMask.GetMask("Factory");
    }

    private void Start()
    {
        if (!PV.IsMine)
        {
            Destroy(GetComponentInChildren<Camera>());
        }
        else
        {
            myCanvas = Instantiate(myCanvas);
            panelInfo = myCanvas.GetComponentInChildren<PanelInfo>();
            factoryPanel = myCanvas.GetComponentInChildren<FactoryPanel>();
            panelInfo.turnOff();
            factoryPanel.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (!PV.IsMine)
            return;

        Ray mousePos = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(mousePos, out hit, 20, maskMob))
        {
            tempObject = hit.collider.gameObject;
            tempObject.GetComponent<MobController>().setParameters(true, mousePos, hit);
        }
        else if(tempObject)
        {
            tempObject.GetComponent<MobController>().setParameters(false, mousePos, hit);

        }


        if(Physics.Raycast(mousePos, out hit, 20, maskMineral))
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                panelInfo.Informations(hit.collider.gameObject.GetComponent<RawMaterials>().count);
            }
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                tempObject.GetComponent<MobController>().dig(hit.collider.gameObject, this.gameObject);
                myCanvas.GetComponentInChildren<Text>().text = countGold.ToString();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
                panelInfo.turnOff();
        }

        if (Physics.Raycast(mousePos, out hit, 20, maskFactory))
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                hit.collider.gameObject.GetComponent<FactoryManager>().toSwitch(factoryPanel.gameObject);
            }
        }
        /*else if(!Physics.Raycast(mousePos, out hit, 20, 5))
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
                factoryPanel.turnOff();
        }*/

        

        move();
    }
    void moveCam()
    {
        float rotY = transform.rotation.eulerAngles.y + Input.GetAxis("Mouse X") * -100;
        float camX = cam.transform.rotation.eulerAngles.x + Input.GetAxis("Mouse Y") * 100;

        Quaternion cam_target = Quaternion.Euler(camX, cam.transform.rotation.eulerAngles.y, 0f);
        Quaternion rot_target = Quaternion.Euler(0f, rotY, 0f);

        cam.transform.rotation = Quaternion.Slerp(cam.transform.rotation, cam_target, Time.deltaTime * 5.0f);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot_target, Time.deltaTime * 5.0f);
    }
    void move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        moveDirection = new Vector3(moveX, 0, moveZ) * speed * Time.deltaTime;
        moveDirection = this.transform.TransformDirection(moveDirection);
        characterController.Move(moveDirection);
    }
}
