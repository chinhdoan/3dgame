using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("Movement")]
        float wsInput, adInput, mouseX, mouseY, xRotation, yRotation;


    [Header("Speed")]
        private Animator anim;
        private Rigidbody rb;
        [SerializeField] private float stillRotateSpeed  = 0.5f;
        [SerializeField] private float walkRotateSpeed = 1.5f;
        [SerializeField] private float runRotateSpeed = 3.5f;
        [SerializeField] private float speed = 5f;
        [SerializeField] GameObject crosshair;
        [SerializeField] Transform orientation;
        private float rotateSpeed;
        private AnimatorStateInfo playerInfo;
        
    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }
    private void Update()
    {
        MyInput();
        MyAnimation();
    }
    private void FixedUpdate()
    {
        MovePlayer();
    }
    void MyInput() {
        wsInput = Input.GetAxis("Vertical");
        adInput = Input.GetAxis("Horizontal");
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
    }

    private void MyAnimation() {
        playerInfo = anim.GetCurrentAnimatorStateInfo(0);

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
            anim.SetBool("walk", true);
        } 
        else if (Input.GetKeyUp(KeyCode.E)) {
            anim.SetBool("walk", false);
        }
      
        if (wsInput > 0)
        {
            anim.SetBool("runBack", false);
            anim.SetBool("run", true);
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
        if (mouseX > 0)
        {
            this.transform.Rotate(Vector3.up * rotateSpeed);
        }
        if (mouseX < 0)
        {
            this.transform.Rotate(Vector3.up * -rotateSpeed);
        }
        yRotation += mouseX;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);


        

        Debug.Log(mouseY);

       
       
    }
    private void MovePlayer()
    {


  
    }
}
