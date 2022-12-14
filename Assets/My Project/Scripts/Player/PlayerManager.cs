using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerManager : MonoBehaviour
{
    [Header("Movement")]
    float wsInput, adInput, mouseX, mouseY;
    Vector3 moveDirection;
    public bool isMoving;



    [Header("GroundCheck")]
    [HideInInspector] public bool isGround = false;
    [SerializeField] float groundDrag;
    public bool isDeathZone;


    [Header("Speed")]
    private Animator anim;
    private Rigidbody rb;
    [SerializeField] private float stillRotateSpeed = 2.5f;
    [SerializeField] private float runRotateSpeed = 4f;
    [SerializeField] private float walkSeed = 10f;
    [SerializeField] private float speed = 3f; //base speed
    [SerializeField] Transform orientaion, mainCameraRotation;
    [HideInInspector] public float rotateSpeed;
    private AnimatorStateInfo playerInfo;
    [SerializeField] private float forceSpeed = 62f; //extra speed
    float forceTime = 0.5f;
    [SerializeField] private float baseForceTimer = 0.1f;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 300f;  //jump height
    [SerializeField] private float airForceSpeed = 7f; //jump far
    [SerializeField] private float jumpTime = 0.25f; //bhop
    [HideInInspector] public float gravity;
    bool isJumping;
    int countJump = 0;
    int tempCount;


    [Header("Crouching")]
    [SerializeField] float crouchSpeed;
    [SerializeField] float crouchYScale;
    private float startYScale;

    [Header("FootStep Sound")]
    [SerializeField] private float baseStepSpeed = 0.5f;
    [SerializeField] private float crouchStepMultipler = 1.5f;
    [SerializeField] private float sprintStepMultipler = 0.6f;
    [SerializeField] private AudioSource footStepAudioSource;
    [SerializeField] AudioClip[] metalClips;
    [SerializeField] AudioClip[] sandClips;
    private float footStepTimer = 0;

    [Header("Bhop")]
    float bhopTimer = 1f;
    float baseBhopTime = 1f;
    float bunnyForce;
    bool isBhop;

    [Header("Finish")]
    [HideInInspector] public bool isFinished;

    GameManager gm = new GameManager();

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
        startYScale = transform.localScale.y;
    }
    private void Update()
    {
        Physics.gravity = new Vector3(0f, -850f, 0f); //Gravity vs JumpFar/  airforce
        MyInput();
        MyAnimation();
        if (isGround)
        {
            rb.drag = groundDrag;
           
            //FixBhopAnimation
            anim.enabled = true;
        }
        else
        {
            rb.drag = 1;
            Physics.gravity = new Vector3(0f, -1190f, 0f); //Gravity vs JumpFar/  airforce
        }
        //Health
        if (SaveSscript.health <= 0)
        {
            Destroy(gameObject);
        }

    }

    private void FixedUpdate()
    {
        //Reduce MoveSpeed Properly *when on ground and air
        if (Input.GetKey(KeyCode.LeftShift)) { forceSpeed = 37f; airForceSpeed = 395f/2; }

        if (!isGround) {
            MovePlayer(5f);
            rb.angularDrag = 9999f;
            forceSpeed = 5f;
        }
        if (isGround) {
            MovePlayer(70f);
            jumpForce = 200f;
            airForceSpeed = 1f;
        }
        if (adInput == 0 && wsInput == 0 ) {
            airForceSpeed = 1f;
        }
    }

    public void MyInput() {
        wsInput = Input.GetAxis("Vertical");
        adInput = Input.GetAxis("Horizontal");
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");


        //Set Jump Once *important settings
        if (Input.GetKeyDown(KeyCode.Space) && isGround && isJumping == false)
        {
            bhopTimer = 1.5f;

            //Double Jump
            if (isGround == false)
            {
                isGround = true;
                Jump(jumpForce);
                MovePlayer(1f);
            }
            //Single Jump
            if (isGround == true)
            {
                airForceSpeed = 430f;
                Jump(jumpForce);
            }
        }

        //SetBhopJumpForce
        if (Input.GetKey(KeyCode.Space) && isGround && isJumping == false && isBhop == true)
        {
            bunnyForce = 230f;
            airForceSpeed = 450f;
            Jump(bunnyForce);
        }

        if (Input.GetKeyDown(KeyCode.LeftControl)) {
            transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
            airForceSpeed = 650f;
            rb.AddForce(Vector3.up*50, ForceMode.Force);    
        }
        if (Input.GetKeyUp(KeyCode.LeftControl)) {
            transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
            //rb.AddForce(Vector3.down * 5, ForceMode.Impulse);
        }
       

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
    public void MovePlayer(float value)
    {
        //Player Movement *Important settings
        moveDirection = orientaion.forward * wsInput + orientaion.right * adInput;
        if (isGround)
        {
            extraSpeed(value);
            if (wsInput > 0 || wsInput < 0 || adInput > 0 || adInput < 0)
            {
                Physics.gravity = new Vector3(0f, -1190f, 0f); //Gravity vs JumpFar/  airforce
                forceSpeed = 60f;
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
            if (wsInput > 0 || wsInput < 0 || adInput > 0 || adInput < 0)
            {
                Physics.gravity = new Vector3(0f, -1190f, 0f);
                forceSpeed = 1f;
            }
            //Set Jump Distance Max
            rb.AddForce(moveDirection.normalized * speed * 1000f * airForceSpeed, ForceMode.Force);
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

    public void Jump(float force)
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * 100 *force , ForceMode.Impulse);
        Physics.gravity = new Vector3(0f, -10f, 0f);
        //isJumping = true;
        Invoke("setJumping", 0.01f);
        isGround = false;
        isJumping = true;
        isBhop = false;
    }
    public void setJumping() {
        isJumping = false; //Set Jumping
        isBhop = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer != 16)
        {
            isDeathZone = false;
        }
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Box" || collision.gameObject.tag == "Sand")
        {
            isGround = true;
            rb.angularDrag = 0.05f;
            airForceSpeed = 0f; //no extra jump length
            countJump = 0;
            Debug.Log("landing");
        }

        //Death Zone
        if (collision.gameObject.layer == 16)
        {
            isDeathZone = true;
        }

        if (collision.gameObject.tag == "FinishLevel")
        {
            isFinished = true;
            GameManager.instance.ActiveFinishPanel();
        }
        if (collision.gameObject.tag == "FinalStep")
        {
            GameManager.instance.LoadNextMap();
            transform.position = GameManager.instance.nextStepSpawn[0].position;
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
                    case "Sand":
                        footStepAudioSource.PlayOneShot(sandClips[Random.Range(0, sandClips.Length - 1)]);
                        break;
                    case "Box":
                        footStepAudioSource.PlayOneShot(sandClips[Random.Range(0, sandClips.Length - 1)]);
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
    void extraSpeed(float foreceSpeed)
    {

            rb.AddForce(moveDirection.normalized * speed * forceSpeed * (1/Time.deltaTime) * 100f, ForceMode.Force);

    }
}
