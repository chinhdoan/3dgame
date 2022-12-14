using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [Header("View")]
    private Animator anim;
    [SerializeField] Transform orientation, playerRotation, bodyRotation;
    [SerializeField] Vector3 offset;
    public bool isActive;


    [Header("Movement")]
    float mouseX, mouseY, xRotation, yRotation;
    [SerializeField] public float senX,senY;

    [Header("ThirdPerson")]
    //[SerializeField] GameObject crosshair;
    [SerializeField] GameObject mainPlayer;
    [SerializeField] GameObject[] handWeaponLocal;

    public static CameraManager instance;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        anim = GetComponent<Animator>();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {

        mouseX = Input.GetAxis("Mouse X") * senX * (1 / Time.deltaTime) * PlayerManager.instance.rotateSpeed;
        mouseY = Input.GetAxis("Mouse Y") * senY * (1 / Time.deltaTime) * PlayerManager.instance.rotateSpeed;

        yRotation += mouseX;

        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);


        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        bodyRotation.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        playerRotation.rotation = Quaternion.Euler(0, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);

       /* if (Input.GetMouseButtonDown(1))
        {
            isActive = true;
            anim.SetBool("aimOrientation", true);
            //crosshair.SetActive(false);
            mainPlayer.SetActive(true);
            handWeaponLocal[SaveSscript.WeaponID].SetActive(false);
        }*/
       /* if (Input.GetMouseButtonUp(1))
        {
            isActive = false;
            anim.SetBool("aimOrientation", false);
            //crosshair.SetActive(true);
            mainPlayer.SetActive(false);
            handWeaponLocal[SaveSscript.WeaponID].SetActive(true);
        }*/
    }
    private void FixedUpdate()
    {
        
       
    }
}