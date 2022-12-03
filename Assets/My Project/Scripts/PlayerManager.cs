using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("Movement")]
    float wsInput, adInput, mouseX, mouseY;
    Vector3 moveDirection;
    bool isMoving;

    [Header("GroundCheck")]
    bool isGround = false;
    [SerializeField] float groundDrag;


    [Header("Speed")]
    private Animator anim;
    private Rigidbody rb;
    [SerializeField] private float stillRotateSpeed = 2.5f;
    [SerializeField] private float runRotateSpeed = 4f;
    [SerializeField] private float walkSeed = 10f;
    [SerializeField] private float speed = 20f;
    [SerializeField] Transform bodyRotation, camRotation;
    [HideInInspector] public float rotateSpeed;
    private AnimatorStateInfo playerInfo;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 300f;
    [SerializeField] private float airForceSpeed = 7f;
    [SerializeField] private float jumpTime = 0.25f;
    bool isJumping;

    [Header("FootStep Sound")]
    [SerializeField] private float baseStepSpeed = 0.5f;
    [SerializeField] private float crouchStepMultipler = 1.5f;
    [SerializeField] private float sprintStepMultipler = 0.6f;
    [SerializeField] private AudioSource footStepAudioSource;
    [SerializeField] AudioClip[] metalClips;
    [SerializeField] AudioClip[] woodClips;
    private float footStepTimer = 0;

    public static PlayerManager instance;
    private void Start()
    {
        instance = this;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        Physics.gravity = new Vector3(0f, -800f, 0f);
    }
    private void Update()
    {
        MyInput();
        MyAnimation();
        Debug.Log(isGround);
       
    }
    private void FixedUpdate()
    {
        ///MyAnimation();
        if (isGround)
        {
            rb.drag = groundDrag;
            anim.enabled = true;
            Jump();
        }
        else
        {
            rb.drag = 0;
            Physics.gravity = new Vector3(0f, -900f, 0f);
            JumpControl();
        }
        MovePlayer();
        SpeedControl();
        

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
            SetAnimationDefault();
            anim.SetBool("run", true);

            setBhopgAnimation();

            if (!isGround)
            {
                anim.SetBool("isJumping", true);
            }
        }
        //only press S
        if (wsInput < 0 && adInput == 0)
        {
            SetAnimationDefault();
            anim.SetBool("runBack", true);

            setBhopgAnimation();

            if (!isGround)
            {
                anim.SetBool("jumpBack", true);
            }
        }
        //only press D
        if (adInput > 0 && wsInput == 0)
        {
            SetAnimationDefault();
            anim.SetBool("runRight", true);

            setBhopgAnimation();

            if (!isGround)
            {
                anim.SetBool("jumpRight", true);
            }

        }
        //only press A
        if (adInput < 0 && wsInput == 0)
        {
            SetAnimationDefault();
            anim.SetBool("runLeft", true);

            setBhopgAnimation();

            if (!isGround)
            {
                anim.SetBool("jumpLeft", true);
            }
        }
        //press WD
        if (wsInput > 0 && adInput > 0)
        {
            SetAnimationDefault();
            anim.SetBool("strafeRight", true);

            setBhopgAnimation();

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

            setBhopgAnimation();

            if (!isGround)
            {
                anim.SetBool("jumpStrafeLeft", true);
            }


        }
        //press SD
        if (wsInput < 0 && adInput < 0)
        {
            SetAnimationDefault();
            anim.SetBool("strafeBackRight", true);

            setBhopgAnimation();

            if (!isGround)
            {
                anim.SetBool("jumpStrafeBackRight", true);
            }
        }
        //press SA
        if (wsInput < 0 && adInput > 0)
        {
            SetAnimationDefault();
            anim.SetBool("strafeBackLeft", true);

            setBhopgAnimation();

            if (!isGround)
            {
                anim.SetBool("jumpStrafeBackLeft", true);
            }
        }
    }
   
    private void SetAnimationDefault() {
        anim.SetBool("run", false);
        anim.SetBool("isJumping", false);
        anim.SetBool("runBack", false);
        anim.SetBool("jumpBack", false);
        anim.SetBool("runRight", false);
        anim.SetBool("jumpRight", false);
        anim.SetBool("jumpStrafeRight", false);
        anim.SetBool("runLeft", false);
        anim.SetBool("jumpLeft", false);
        anim.SetBool("jumpStrafeLeft", false);
        anim.SetBool("strafeRight", false);
        anim.SetBool("jumpStrafeRight", false);
        anim.SetBool("strafeLeft", false);
        anim.SetBool("jumpStrafeLeft", false);
        anim.SetBool("strafeBackLeft", false);
        anim.SetBool("jumpStrafeBackLeft", false);
        anim.SetBool("strafeBackRight", false);
        anim.SetBool("jumpStrafeBackRight", false);
    }
    private void MovePlayer()
    {

        moveDirection = camRotation.forward * wsInput + camRotation.right * adInput;
       
        if (isGround)
        {
            rb.AddForce(moveDirection.normalized * speed * (1 / Time.deltaTime) * 10f, ForceMode.Force);
            if (wsInput > 0 || wsInput < 0 || adInput > 0 || adInput < 0)
            {
                isMoving = true;
            }
            else {
                isMoving = false;
            }
            if (isMoving == true)
            {
                HandleSound();
            }
        }

        else if (!isGround)
        {
            rb.AddForce(moveDirection.normalized * speed * 100f * airForceSpeed, ForceMode.Force);

            HandleSound();
            //rb.AddForce(moveDirection.normalized * -(speed / 2) * 10f * (-airForceSpeed / 2), ForceMode.Force);
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
        if (Input.GetKey(KeyCode.Space) && isGround && isJumping == false) {
            isJumping = true;
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            rb.AddForce(transform.up* jumpForce, ForceMode.Impulse);

            //isJumping = true;
            Invoke("setJumping", 0.5f);
            Debug.Log("jumping");
            isGround = false;
        }  
    }
    void setJumping() {
        //Set Bhop Animation
        SetAnimationDefault();
        anim.enabled = false;
        //Set Jumping
        isJumping = false;
    }
    void setBhopgAnimation() {
        if (!isGround && Input.GetKey(KeyCode.Space))
        {
            SetAnimationDefault();
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
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Box")
        {
            isGround = true;
            Debug.Log("landing");
        }
    }
    void HandleSound()
    {
        if (!isGround) return;
        footStepTimer -= Time.deltaTime;
        if (footStepTimer <= 0) {
            if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 3)) {
                switch(hit.collider.tag){
                    case "Ground":
                        footStepAudioSource.PlayOneShot(metalClips[Random.Range(0,metalClips.Length-1)]);
                        break;
                }
            }
            footStepTimer = baseStepSpeed * 0.6f;
        }
    }
}
