using Photon.Pun;
using Photon.Pun.Demo.PunBasics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviourPunCallbacks, IPunObservable
{
    CharacterController characterController;
    Vector3 moveDirection;
    [SerializeField] float speed;
    Camera cam;
    public static GameObject LocalPlayerInstance;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        
    }


    public void Awake()
    {
        if (photonView.IsMine) LocalPlayerInstance = gameObject;

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        cam = GetComponentInChildren<Camera>();
        characterController.detectCollisions = false;

        CameraWork _cameraWork = gameObject.GetComponent<CameraWork>();

        if (_cameraWork != null)
        {
            if (photonView.IsMine) _cameraWork.OnStartFollowing();
        }
        else Debug.LogError("<Color=Red><a>Missing</a></Color> CameraWork Component on playerPrefab.", this);
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");      
        float moveZ= Input.GetAxis("Vertical");
        moveDirection = new Vector3(moveX, 0, moveZ) * speed * Time.deltaTime;
        moveDirection = this.transform.TransformDirection(moveDirection);
        characterController.Move(moveDirection);

        if (Input.GetKey(KeyCode.Mouse1))
        {
            float rotY = transform.rotation.eulerAngles.y + Input.GetAxis("Mouse X") * -100;
            float camX = cam.transform.rotation.eulerAngles.x + Input.GetAxis("Mouse Y") * 100;

            Quaternion cam_target = Quaternion.Euler(camX, cam.transform.rotation.eulerAngles.y, 0f);
            Quaternion rot_target = Quaternion.Euler(0f, rotY, 0f);

            cam.transform.rotation = Quaternion.Slerp(cam.transform.rotation, cam_target, Time.deltaTime * 5.0f);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot_target, Time.deltaTime * 5.0f);
        }

    }
}
