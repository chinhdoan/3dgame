using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("Movement")]
        float wsInput;
        float adInput;
        float rotateDirection;

    [Header("Speed")]
        private Animator anim;
        private Rigidbody rb;
        [SerializeField] private float stillRotateSpeed  = 0.5f;
        [SerializeField] private float walkRotateSpeed = 1.5f;
        [SerializeField] private float runRotateSpeed = 3.5f;
        [SerializeField] private float speed = 5f;
        [SerializeField] Transform orientation;
        private float rotateSpeed;
        private AnimatorStateInfo playerInfo;
        
    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
 /*       Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Locked;*/
    }
    private void Update()
    {
        MyAnimation();
    }
    private void FixedUpdate()
    {
        MovePlayer();
    }
    private void MyAnimation() {
        playerInfo = anim.GetCurrentAnimatorStateInfo(0);
        wsInput = Input.GetAxis("Vertical");
        adInput = Input.GetAxis("Horizontal");
        rotateDirection = Input.GetAxis("Mouse X");

        if (playerInfo.IsTag("Still"))
        {
            rotateSpeed = stillRotateSpeed;
        }
        if (playerInfo.IsTag("Walk"))
        {
            rotateSpeed = walkRotateSpeed;
        }
        if (playerInfo.IsTag("Run"))
        {
            rotateSpeed = runRotateSpeed;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            anim.SetBool("walk",true);
        }
       
        if (Input.GetKeyUp(KeyCode.E)) {
            anim.SetBool("walk", false);
        }
       
        if (wsInput > 0)
        {
            anim.SetBool("runBack", false);
            anim.SetBool("run", true);
        }
        if (rotateDirection > 0)
        {
            this.transform.Rotate(Vector3.up * rotateSpeed );
        }
        if (wsInput == 0)
        {
            anim.SetBool("run", false);
            anim.SetBool("runBack", false);
        }
        if (wsInput < 0)
        {
            anim.SetBool("run", false);
            anim.SetBool("runBack", true);
        }
        if (rotateDirection < 0)
        {
            this.transform.Rotate(Vector3.up * -rotateSpeed );
        }
    }
    private void MovePlayer()
    {
        Vector3 moveDirection = orientation.forward * wsInput + orientation.right * adInput;
        rb.AddForce(moveDirection.normalized * speed, ForceMode.Force);
    }
}
