using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("Movement")]
    float wsInput, adInput, mouseX, mouseY;
    Vector3 moveDirection;

    [Header("GroundCheck")]
    [SerializeField] float playerHeihgt;
    [SerializeField] LayerMask Wall;
    static bool isGround;
    [SerializeField] float groundDrag;


    [Header("Speed")]
    private Animator anim;
    private Rigidbody rb;
    [SerializeField] private float stillRotateSpeed = 0.5f;
    [SerializeField] private float runRotateSpeed = 3.5f;
    [SerializeField] private float walkSeed = 0f;
    [SerializeField] private float speed = 5f;
    [SerializeField] Transform bodyRotation, camRotation;
    private float rotateSpeed;
    private AnimatorStateInfo playerInfo;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 550f;
    [SerializeField] private float airForceSpeed = 0.4f;
    [SerializeField] private float jumpTime = 0.25f;
    bool isJumping;

    [Header("Sound")]
        [SerializeField] AudioClip footStepSound;
        private AudioSource moveAudio;
    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        //Physics.gravity = new Vector3(0f, -900f, 0f);
    }
    private void Update()
    {

        isGround = Physics.Raycast(transform.position, Vector3.down, 1+0.001f);
        MyInput();
        MyAnimation();
        Debug.Log(isGround);
       
    }
    private void FixedUpdate()
    {
        MovePlayer();
        SpeedControl();
        if (isGround)
        {
            rb.drag = groundDrag;
           /* if (jumpForce > 0)
            {
                jumpForce -= 1500f * Time.deltaTime;
            }*/
            Jump();
            
        }
        else
        {
            JumpControl();
            rb.drag = 0;
            /*if (jumpForce < 0)
            {
                jumpForce -= 1000f * Time.deltaTime;
                //Physics.gravity = new Vector3(0f, -900f, 0f);
            }   */
        }

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
        //release 
        if (wsInput == 0 || adInput == 0)
        {
            SetAnimationDefault();
            
        }
        //only press W
        if (wsInput > 0 && adInput == 0)
        {
            anim.SetBool("runBack", false);
            anim.SetBool("run", true);

            if (!isGround)
            {
                anim.SetBool("isJumping", true);
            }

        }
        //only press S
        if (wsInput < 0 && adInput == 0)
        {
            anim.SetBool("run", false);
            anim.SetBool("runBack", true);
 
            if (!isGround)
            {
                anim.SetBool("jumpBack", true);
            }
        }
        //only press D
        if (adInput > 0)
        {
            SetAnimationDefault();
            anim.SetBool("runRight", true);
 

        }
        //only press A
        if (adInput < 0)
        {
            SetAnimationDefault();
            anim.SetBool("runLeft", true);
        }
        //press WD
        if (wsInput > 0 && adInput > 0)
        {
            SetAnimationDefault();
            anim.SetBool("strafeRight", true);

            if (!isGround)
            {
                anim.SetBool("jumpStrafeRight", true);
            }
        }
        //press WA
        if (wsInput > 0 && adInput < 0)
        {
            SetAnimationDefault();
            anim.SetBool("strafeLeft", true);

            if (!isGround)
            {
                anim.SetBool("jumpStrafeLeft", true);
            }


        }
        //press SD
        if (wsInput < 0 && adInput < 0)
        {
            SetAnimationDefault();
            anim.SetBool("strafeBackLeft", true);

        }
        //press SA
        if (wsInput < 0 && adInput > 0)
        {
            SetAnimationDefault();
            anim.SetBool("strafeBackRight", true);

        }
    }
   
    private void SetAnimationDefault() {
        anim.SetBool("run", false);
        anim.SetBool("runBack", false);
        anim.SetBool("runRight", false);
        anim.SetBool("runLeft", false);
        anim.SetBool("strafeRight", false);
        anim.SetBool("strafeLeft", false);
        anim.SetBool("strafeBackLeft", false);
        anim.SetBool("strafeBackRight", false);
        anim.SetBool("isJumping", false);
        anim.SetBool("jumpBack", false);
        anim.SetBool("jumpStrafeRight", false);
        anim.SetBool("jumpStrafeLeft", false);
    }
    private void MovePlayer()
    {

        moveDirection = camRotation.forward * wsInput + camRotation.right * adInput;
        if (isGround)
        {
            rb.AddForce(moveDirection.normalized * speed * 10f, ForceMode.Force);
            rb.AddForce(moveDirection.normalized * -(speed / 2) * 10f * (-airForceSpeed / 2), ForceMode.Force);
        }

        else if (!isGround)
        {
            airForceSpeed = 1f;
            rb.AddForce(moveDirection.normalized * speed * 10f * airForceSpeed, ForceMode.Force);

            rb.AddForce(moveDirection.normalized * -(speed / 2) * 10f * (-airForceSpeed / 2), ForceMode.Force);
        }

    }

    private void SpeedControl() {
        Vector3 flatVel = new Vector3(rb.velocity.x,0f, rb.velocity.z);

        if (flatVel.magnitude > speed)
        {
            Vector3 limitVel = flatVel.normalized * speed;
            rb.velocity = new Vector3(limitVel.x,rb.velocity.y,limitVel.z);
        }
    }

    private void Jump()
    {
        isJumping = false;
        if (Input.GetKeyDown(KeyCode.Space) && isJumping) {
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            rb.AddForce(transform.up* jumpForce, ForceMode.Impulse);
            isJumping = true;
        }  
    }

    private void JumpControl()
    {
        Vector3 jumpVel = new Vector3(0f, rb.velocity.y, 0f);

        if (jumpVel.magnitude > jumpForce)
        {
            Vector3 limitVel = jumpVel.normalized * jumpForce;
            rb.velocity = new Vector3(rb.velocity.x, limitVel.y, rb.velocity.z);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            isGround = true;
        }
    }
    /*IEnumerator  waitSound() {
        moveAudio = GetComponent<AudioSource>();
        moveAudio.clip = footStepSound;
        moveAudio.Play();
        yield return new WaitForSeconds(3f);
        moveAudio.Stop();
        yield return new WaitForSeconds(1f);

    }*/
}
