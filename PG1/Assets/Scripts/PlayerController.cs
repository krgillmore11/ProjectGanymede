using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
//Handles all ingame Movment for player character
//ADD SOUNDS AND CHANGE HEARING DISTANCE FOR ENEMY

public class PlayerController : MonoBehaviour{
    private PlayerInput input;
    private Rigidbody rb;
    private bool sprinting = false;
    private bool sprintOnCooldown = false;
    public float sprintTimer = 0f;
    //public float sprintCooldownTimer = 0f;
    private bool grounded;
    public bool isPlayingAnimation = false;
    public Transform playerCam;
    private float rotationX = 0f;
    private Vector2 lookInput;
    private Animator anim;
    public AudioSource walkSource;
    private AudioSource aSource;
    private float lastStepTime;
    private PlayerManager pm;
    private ObjectPickup op;

   


    [SerializeField] Transform arms;
    public Camera cameraObject;
    

    [SerializeField] float baseMoveSpeed = 5f;
    [SerializeField] float jumpHeight = 7f;
    [SerializeField] float sprintMultiplier = 1.5f;
    [SerializeField] float mouseSensitivity = 2f;
    [SerializeField] float deceleration = 2f;
    //[SerializeField] float maxSpeed = 1f;
    [SerializeField] float interactDistance = 3f;
    [SerializeField] AudioClip walkingSound;
    [SerializeField] AudioClip runningSound;
    [SerializeField] AudioClip shootingSound;
    [SerializeField] AudioClip punchingSound;
    [SerializeField] AudioClip interactingSound;
    [SerializeField] PauseMenu psm;
    [SerializeField] float sprintDuration = 5f;  
    [SerializeField] float sprintCooldown = 10f;

    void Awake(){//Awake is good for setting up references

        op = GetComponent<ObjectPickup>();

        pm = GetComponent<PlayerManager>();
        //walkSource = GetComponentInChildren<AudioSource>();
        aSource = GetComponent<AudioSource>();
        //stepSource.enabled = false;

        rb = GetComponent<Rigidbody>();
        input = new PlayerInput();
        anim = GetComponentInChildren<Animator>();
        //playerCam = transform.Find("Player Camera"); 
        Cursor.lockState = CursorLockMode.Locked;//Lock cursor off screen
        input.Player.Sprint.performed += SprintToggle; 
    }

    void FixedUpdate(){  //Use for physics and rb ONLY
        CameraRotation();    
        PlaySounds();
        SprintTimer();

        //Debug.Log(rb.velocity);
        
        //Get movement input
        Vector2 moveInput = input.Player.Move.ReadValue<Vector2>();

        //set move direction to camera
        Vector3 moveDirection = (transform.forward * moveInput.y) + (transform.right * moveInput.x);
        moveDirection.y = 0f; //disables vertical movement when not jumping, do I need this?

        //apply movement forces
        //sprinting
        float currentMoveSpeed;
        if (sprinting){
            currentMoveSpeed = baseMoveSpeed * sprintMultiplier;
        }
        else{
            currentMoveSpeed = baseMoveSpeed;
        }
        Vector3 horizontalForce = moveDirection.normalized * currentMoveSpeed;

        //Calculate velocity change to max
        Vector3 velocityChange = (horizontalForce - rb.velocity);
        velocityChange.x = Mathf.Clamp(velocityChange.x, -currentMoveSpeed, currentMoveSpeed);
        velocityChange.z = Mathf.Clamp(velocityChange.z, -currentMoveSpeed, currentMoveSpeed);
        velocityChange.y = 0f;

        //use addforce so it works w gravity
        //Apply force for movement only if there is input
        if (moveDirection.magnitude > .1f ){
        rb.AddForce(velocityChange, ForceMode.VelocityChange);
        } 
        else{
        //apply stoppping force to avoid ice rink
        Vector3 oppositeVelocity = -rb.velocity;
        oppositeVelocity.y = 0f; //Ignore vertical velocity
        rb.AddForce(oppositeVelocity * currentMoveSpeed * deceleration, ForceMode.Acceleration);
        }

        //jump
        if(input.Player.Jump.triggered && grounded){
            grounded = false;
            rb.AddForce(Vector3.up * jumpHeight / Time.fixedDeltaTime, ForceMode.Impulse);
            anim.SetTrigger("Jump");
        }

        //exceeding speed along the horizontal plane
        /*
        Vector3 horizontalVelocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        float exceedingSpeed = horizontalVelocity.magnitude - maxSpeed;

        //if the character is moving too fast, apply a counteracting force
        if (exceedingSpeed > 0f){
        Vector3 counteractingForce = horizontalVelocity.normalized * exceedingSpeed;
        rb.AddForce(counteractingForce, ForceMode.Acceleration);
        }
        */
        HandleAnimation(moveDirection);
    }

    void Update(){
        Interaction();
    }

    void PlaySounds(){
        if((rb.velocity.x > .1f || rb.velocity.x < -.1f || rb.velocity.y > .1f || rb.velocity.y < -.1f) && grounded){
                AudioClip steps;
                if(sprinting){
                    steps = runningSound;
                
                }
                else{
                    steps = walkingSound;
                }
                if (walkSource.clip != steps || !walkSource.isPlaying){
   
                    walkSource.Stop();

                    walkSource.clip = steps;
                    walkSource.Play();
        }
    }
    else
    {
    
        if (walkSource.isPlaying){
            walkSource.Stop();
        }
    }
    }

    private void Interaction(){
        if (!isPlayingAnimation){
            if(input.Player.Interact.triggered){
                Interact();
            }

            if(input.Player.Attack.triggered && !op.holdingItem){
                Shoot();
                isPlayingAnimation = true;
            }

            if(input.Player.Attack2.triggered){
                Punch();
            }

            if(input.Player.Menu.triggered){
                psm.TogglePauseMenu();
            }
        }
        
    }

    private void CameraRotation(){
        //get rotation from cam
        lookInput = input.Player.Look.ReadValue<Vector2>();
        float mouseX = lookInput.x * mouseSensitivity * Time.deltaTime;
        float mouseY = lookInput.y * mouseSensitivity * Time.deltaTime;

        //apply it
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);
        playerCam.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
        arms.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
        transform.Rotate(Vector3.up, mouseX);
    }

    private void HandleAnimation(Vector3 moveDir){
        if(moveDir != Vector3.zero && !sprinting){
            anim.SetFloat("Speed", .25f, .2f, Time.deltaTime);
        }
        else if(moveDir != Vector3.zero && sprinting){
            anim.SetFloat("Speed", .5f, .2f, Time.deltaTime);
            //Debug.Log("animSprintPlaying");
        }
        else{
            anim.SetFloat("Speed", 0f, .2f, Time.deltaTime);
        }
    }

    private void SprintToggle(InputAction.CallbackContext context){
        if (context.performed && !sprintOnCooldown){
            sprinting = !sprinting;
            if (sprinting){
                sprintTimer = sprintDuration;
            }
            //Debug.Log(sprinting);
        }
    }

    private void Interact(){
        //Debug.Log("interaction completed");    
        if(op.holdingItem){
            op.DropItem();
        }
        else{
            Ray ray = new Ray(cameraObject.transform.position, cameraObject.transform.forward);//ray shooting directly from camera
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, interactDistance)){
                if(hit.collider.CompareTag("Pickup") && !op.holdingItem){
                    op.PickupItem(hit.collider.gameObject);
                }

                IInteractable interactable = hit.collider.GetComponent<IInteractable>();
                if(interactable != null){
                    isPlayingAnimation = true;
                    anim.SetTrigger("Interact");
                    aSource.PlayOneShot(interactingSound);
                    interactable.Interact();
                }
            }
        }
    }

    private void Shoot(){
        anim.SetTrigger("Shoot");
        aSource.PlayOneShot(shootingSound);
        pm.Shoot();
    }

    private void Punch(){
        if(op.holdingItem){
            op.Throw();
        }
        else{
        isPlayingAnimation = true;
        anim.SetTrigger("Punch");
        aSource.PlayOneShot(punchingSound);
        pm.Punch();
        }
    }

    public bool IsSprinting(){
        return sprinting;
    }

    void SprintTimer(){
        if (sprinting){
            sprintTimer -= Time.fixedDeltaTime;
            if (sprintTimer <= 0f){
                sprinting = false;
                sprintTimer = 0f;
                StartCoroutine(StartSprintCooldown());
            }
        }
    }

    private IEnumerator StartSprintCooldown(){
        sprintOnCooldown = true;
        yield return new WaitForSeconds(sprintCooldown);
        sprintOnCooldown = false;
}

    private void OnCollisionEnter(Collision collision){
        if (collision.gameObject.CompareTag("Ground")){
            grounded = true;
        }
    }

    private void OnCollisionExit(Collision collision){
        if (collision.gameObject.CompareTag("Ground")){
            grounded = false;
        }
    }

    private void OnEnable(){
        input.Enable();
    }

    private void OnDisable(){
        input.Disable();
    }
}
