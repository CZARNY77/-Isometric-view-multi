using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController characterController;
    Vector3 moveDirection;
    [SerializeField] float speed;
    Camera cam;
    PhotonView PV;

    [SerializeField] GameObject Objective;
    LayerMask mask;
    [SerializeField] Material hover;
    [SerializeField] Material select;
    [SerializeField] Material normal;
    GameObject tempObject;
    bool selected = false;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        cam = GetComponentInChildren<Camera>();
        characterController.detectCollisions = false;
        PV = GetComponentInChildren<PhotonView>();
        mask = LayerMask.GetMask("Mob");

    }

    private void Start()
    {
        if (!PV.IsMine)
        {
            Destroy(GetComponentInChildren<Camera>());
        }
    }

    void Update()
    {
        if (!PV.IsMine)
            return;

        Ray mousePos = cam.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(mousePos.origin, mousePos.direction*20, Color.red);
        RaycastHit hit;
        mask = LayerMask.GetMask("Mob");
        if (Physics.Raycast(mousePos.origin, mousePos.direction * 20, out hit, Mathf.Infinity, mask) && !selected)
        {
            tempObject = hit.collider.gameObject;
            hit.collider.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material = hover;
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                hit.collider.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material = select;
                selected = true;
            }
        }
        else
        {
            if(tempObject && !selected)
            {
                tempObject.GetComponentInChildren<SkinnedMeshRenderer>().material = normal;
                tempObject = null;
            }

            if (Input.GetKeyDown(KeyCode.Mouse0) && selected)
            {
                
                selected = false;
                mask = LayerMask.GetMask("Floor");
                if (Physics.Raycast(mousePos.origin, mousePos.direction * 20, out hit, Mathf.Infinity, mask))
                    Instantiate(Objective, mousePos.direction * hit.distance + mousePos.origin + new Vector3(0, 0.1f, 0), Quaternion.Euler(90f, 0, 0));
            }
        }

        move();

        if (Input.GetKey(KeyCode.Mouse1))
        {
            moveCam();
        }
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
