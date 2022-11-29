using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private Animator anim;
    [SerializeField] Transform bodyRotation, camPos, playerRotation;
    [SerializeField] Vector3 offset;


    [Header("Movement")]
        float wsInput, adInput, mouseX, mouseY, xRotation, yRotation;
        float xAngle = 0;
        [SerializeField] float senX,senY;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * senX;
        mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * senY;

        yRotation += mouseX;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        bodyRotation.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        playerRotation.rotation = Quaternion.Euler(0, yRotation, 0);
        if (Input.GetMouseButtonDown(1))
        {
            anim.SetBool("aimOrientation", true);
        }
        if (Input.GetMouseButtonUp(1))
        {
            anim.SetBool("aimOrientation", false);
        }


    }
}
