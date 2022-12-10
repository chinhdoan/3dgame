using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("Movement")]
    float wsInput, adInput, mouseX, mouseY;
    Vector3 moveDirection;
    public bool isMoving;

    [Header("GroundCheck")]
    [HideInInspector] public bool isGround = false;
    [SerializeField] float groundDrag;


    [Header("Speed")]
    private Animator anim;
    private Rigidbody rb;
    [SerializeField] private float stillRotateSpeed = 2.5f;
    [SerializeField] private float runRotateSpeed = 4f;
    [SerializeField] private float walkSeed = 10f;
    [SerializeField] private float speed = 20f;
    [SerializeField] Transform orientaion, mainCameraRotation;
    [HideInInspector] public float rotateSpeed;
    private AnimatorStateInfo playerInfo;
    [SerializeField] private float forceSpeed = 0.5f;
    float forceTime = 0.5f;
    [SerializeField] private float baseForceTimer = 0.1f;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 300f;
    [SerializeField] private float airForceSpeed = 7f;
    [SerializeField] private float jumpTime = 0.25f;
    [HideInInspector] public float gravity;
    bool isJumping;
    int countJump = 0;
    int tempCount;

    [Header("FootStep Sound")]
    [SerializeField] private float baseStepSpeed = 0.5f;
    [SerializeField] private float crouchStepMultipler = 1.5f;
    [SerializeField] private float sprintStepMultipler = 0.6f;
    [SerializeField] private AudioSource footStepAudioSource;
    [SerializeField] AudioClip[] metalClips;
    [SerializeField] AudioClip[] woodClips;
    private float footStepTimer = 0;

    [Header("Bhop")]
    float bhopTimer = 1f;
    float baseBhopTime = 1f;
    float bunnyForce;
    public bool isBhop;

    public static PlayerManager instance;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance == this)
        {
            Destroy(instance);
        }
        else {
            DontDestroyOnLoad(this);
        }
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        gravity = -1100f;
        Physics.gravity = new Vector3(0f, gravity, 0f);
    }
    private void Update()
    {

        MyInput();

        if (isGround)
        {
            rb.drag = groundDrag;

            //FixBhopAnimation
            anim.enabled = true;
        }
        else
        {
            rb.drag = 1;
        }
       
       
    }

    private void FixedUpdate()
    {
       
        //Set Jump Once
        if (Input.GetKeyDown(KeyCode.Space) && isGround && isJumping == false)
        {
            bhopTimer = 1.5f;

            //Double Jump
            if (isGround == false)
            {
                isGround = true;
                jumpForce = 250f;
                Jump(jumpForce);
            }
            //Single Jump
            if (isGround == true)
            {
                jumpForce = 300f;
                Jump(jumpForce);
            }
        }

        //SetBhopJumpForce
        if (Input.GetKey(KeyCode.Space) && isGround && isJumping == false)
        {
            bunnyForce = 350f;
            Jump(bunnyForce);
        }
        MovePlayer();
        MyAnimation();
    }

    public void MyInput() {
        wsInput = Input.GetAxis("Vertical");
        adInput = Input.GetAxis("Horizontal");
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
    }

    public void MyAnimation() {
        anim.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("FixBhopAnimation/PlayerController");
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

            if (!isGround)
            {
                anim.SetBool("isJumping", true);
                BhopDelayTimer();
            }
        }
        //only press S
        if (wsInput < 0 && adInput == 0)
        {
            SetAnimationDefault();
            anim.SetBool("runBack", true);

            if (!isGround)
            {
                anim.SetBool("jumpBack", true);
                BhopDelayTimer();
            }




        }
        //only press D
        if (adInput > 0 && wsInput == 0)
        {
            SetAnimationDefault();
            anim.SetBool("runRight", true);


            if (!isGround)
            {
                anim.SetBool("jumpRight", true);
                BhopDelayTimer();
            }

        }
        //only press A
        if (adInput < 0 && wsInput == 0)
        {
            SetAnimationDefault();
            anim.SetBool("runLeft", true);


            if (!isGround)
            {
                anim.SetBool("jumpLeft", true);
                BhopDelayTimer();
            }


        }
        //press WD
        if (wsInput > 0 && adInput > 0)
        {
            SetAnimationDefault();
            anim.SetBool("strafeRight", true);


            if (!isGround)
            {
                anim.SetBool("jumpStrafeRight", true);
                BhopDelayTimer();
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
                BhopDelayTimer();
            }



        }
        //press SA
        if (wsInput < 0 && adInput < 0)
        {
            SetAnimationDefault();
            anim.SetBool("strafeBackRight", true);


            if (!isGround)
            {
                anim.SetBool("jumpStrafeBackRight", true);
                BhopDelayTimer();
            }

        }
        //press SD
        if (wsInput < 0 && adInput > 0)
        {
            SetAnimationDefault();
            anim.SetBool("strafeBackLeft", true);


            if (!isGround)
            {
                anim.SetBool("jumpStrafeBackLeft", true);
                BhopDelayTimer();
            }
        }
    }

    public void SetAnimationDefault() {
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
    public void MovePlayer()
    {
        //Player Movement
        moveDirection = orientaion.forward * wsInput + orientaion.right * adInput;
        if (isGround)
        {
            rb.AddForce(moveDirection.normalized * speed * (1 / Time.deltaTime) * 10f, ForceMode.Force);
            forceTime -= Time.deltaTime;
            if (forceTime <= 0) {
                rb.AddForce(moveDirection.normalized * speed * forceSpeed * 10f, ForceMode.Force);
            }
            if (wsInput > 0 || wsInput < 0 || adInput > 0 || adInput < 0)
            {
                isMoving = true;
                //bhopTimer = 1.5f;
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
            if (wsInput < 0)
            {
                airForceSpeed = 15f;
            }
            if (wsInput > 0 && adInput > 0 || wsInput > 0 && adInput < 0)
            {
                airForceSpeed = 15f;
            }
            if (wsInput > 0 || adInput > 0 || adInput < 0) {
                airForceSpeed = 14.25f;
            }
            //Set Jump Distance Max
            rb.AddForce(moveDirection.normalized * speed * 100f * airForceSpeed, ForceMode.Force);
            HandleSound();
        }
        if (wsInput == 0 && adInput == 0 || Input.GetKeyDown(KeyCode.Space)){
            forceTime = baseForceTimer;
        }

       
        SpeedControl();
    }

    public void SpeedControl() {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVel.magnitude > speed)
        {
            Vector3 limitVel = flatVel.normalized * speed;
            rb.velocity = new Vector3(limitVel.x, rb.velocity.y, limitVel.z);
        }
    }

    public void JumpPlayer()
    {
         //Set Jump Once
        if (Input.GetKeyDown(KeyCode.Space) && isGround && isJumping == false)
        {
            bhopTimer = 1.5f;

            //Double Jump
            if (isGround == false)
            {
                isGround = true;
                jumpForce = 250f;
                Jump(jumpForce);
            }
            //Single Jump
            if (isGround == true)
            {
                jumpForce = 300f;
                Jump(jumpForce);
            }
        }

        //SetBhopJumpForce
        if (Input.GetKey(KeyCode.Space) && isGround && isJumping == false)
        {
            bunnyForce = 350f;
            Jump(bunnyForce);
        }
    }

    public void Jump(float force)
    {
        isJumping = true;
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * force , ForceMode.Impulse);
        Physics.gravity = new Vector3(0f, -10f, 0f);
        //isJumping = true;
        Invoke("setJumping", 0.01f);
        isGround = false;

        
    }
    public void setJumping() {
        countJump++;
        Physics.gravity = new Vector3(0f, -850f, 0f);
        isJumping = false; //Set Jumping
    }


    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Box")
        {
            isGround = true;
            countJump = 0;
            Debug.Log("landing");
        }
    }
    public void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Box")
        {
            isBhop = false;
        }
    }

    public void HandleSound()
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
    public void BhopDelayTimer() {
        bhopTimer -= Time.deltaTime;
        if (bhopTimer <= 0) {
            anim.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("FixBhopAnimation/bhop");//Set Bhop Animation 
            anim.enabled = false;
        }
    }
}
